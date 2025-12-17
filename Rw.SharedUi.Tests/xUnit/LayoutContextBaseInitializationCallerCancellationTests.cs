using Rw.SharedUi.Layout;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseInitializationCallerCancellationTests
{
    [Fact(Timeout = 5_000)]
    public async Task InitializeAsync_CallerCancellation_CancelsWaitingButInitializationContinues()
    {
        var ctx = new BlockingInitLayoutContext();

        var initTask = ctx.InitializeAsync(CancellationToken.None);      // start init (will block)
        await ctx.Entered;                         // init has entered OnInitializeAsync

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        try
        {
            // this caller should be cancelled while waiting
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => ctx.InitializeAsync(cts.Token));

            // init should still be running (not completed) until release
            Assert.Equal(1, ctx.InitCallCount);
            Assert.False(initTask.IsCompleted);
        }
        finally
        {
            ctx.Release();
        }

        await initTask;

        // after init completed, future calls should be completed and not re-run init
        await ctx.InitializeAsync(CancellationToken.None);
        Assert.Equal(1, ctx.InitCallCount);
    }
}
