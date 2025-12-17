namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseInitializationContractTests
{
    [Fact]
    public async Task InitializeAsync_ConcurrentCaller_DoesNotCompleteWhileInitStillRunning()
    {
        var ctx = new BlockingInitLayoutContext();

        var t1 = Task.Run(() => ctx.InitializeAsync(CancellationToken.None));
        await ctx.Entered; // init is started and blocking

        var t2 = ctx.InitializeAsync(CancellationToken.None);

        Assert.False(t2.IsCompleted, "Second InitializeAsync completed while init is still running.");

        ctx.Release();
        await Task.WhenAll(t1, t2);

        Assert.Equal(1, ctx.InitCallCount);
    }
}