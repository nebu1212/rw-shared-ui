namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseInitializationConcurrencyTests
{
    [Fact(Timeout = 5_000)]
    public async Task InitializeAsync_SecondCallWhileInitRunning_ReturnsSameTask_AndDoesNotCompleteEarly()
    {
        var ctx = new BlockingInitLayoutContext();

        var t1 = ctx.InitializeAsync(CancellationToken.None);
        await ctx.Entered; // init is inside OnInitializeAsync and blocked

        var t2 = ctx.InitializeAsync(CancellationToken.None);

        Assert.Same(t1, t2);
        Assert.False(t2.IsCompleted, "Second InitializeAsync must not complete while initialization is still running.");
        Assert.Equal(1, ctx.InitCallCount);

        try
        {
            // release and complete
            ctx.Release();
            await Task.WhenAll(t1, t2);
        }
        finally
        {
            // idempotent safety: calling Release twice should not throw
            ctx.Release();
        }

        Assert.Equal(1, ctx.InitCallCount);
    }

    [Fact(Timeout = 5_000)]
    public async Task InitializeAsync_AfterInitCompleted_ReturnsCompletedTask_AndDoesNotReinitialize()
    {
        var ctx = new BlockingInitLayoutContext();

        var t1 = ctx.InitializeAsync(CancellationToken.None);
        await ctx.Entered;

        ctx.Release();
        await t1;

        var t2 = ctx.InitializeAsync(CancellationToken.None);

        Assert.True(t2.IsCompleted);
        Assert.Equal(1, ctx.InitCallCount);
    }
}
