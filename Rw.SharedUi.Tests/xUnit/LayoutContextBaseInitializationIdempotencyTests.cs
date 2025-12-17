namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseInitializationIdempotencyTests
{
    [Fact]
    public async Task InitializeAsync_CalledTwice_InvokesCoreOnce()
    {
        var ctx = new TestLayoutContext();

        await ctx.InitializeAsync(CancellationToken.None);
        await ctx.InitializeAsync(CancellationToken.None);

        Assert.Equal(1, ctx.InitCallCount);
    }

    [Fact(Timeout = 5_000)]
    public async Task InitializeAsync_ManyConcurrentCalls_WhileInitIsRunning_AllAwaitSameInitialization()
    {
        var ctx = new BlockingInitLayoutContext();

        // Start init and block inside OnInitializeAsync
        var first = ctx.InitializeAsync(CancellationToken.None);
        await ctx.Entered;

        // Now flood with concurrent callers while init is still running
        var callers = Enumerable.Range(0, 50)
            .Select(_ => ctx.InitializeAsync(CancellationToken.None))
            .ToArray();

        // None should be completed yet (they all depend on the same blocked init)
        Assert.All(callers, t => Assert.False(t.IsCompleted));
        Assert.Equal(1, ctx.InitCallCount);

        ctx.Release();

        await Task.WhenAll(callers.Append(first));

        Assert.Equal(1, ctx.InitCallCount);
    }
}
