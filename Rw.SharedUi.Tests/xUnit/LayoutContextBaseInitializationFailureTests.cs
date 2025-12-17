using Rw.SharedUi.Layout;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseInitializationFailureTests
{
    private sealed class AlwaysFailLayoutContext : LayoutContextBase
    {
        public int InitCallCount { get; private set; }

        protected override Task OnInitializeAsync()
        {
            InitCallCount++;
            throw new InvalidOperationException("always fail");
        }
    }

    private sealed class FailOnceLayoutContext : LayoutContextBase
    {
        private bool _failedOnce;

        public int InitCallCount { get; private set; }

        protected override Task OnInitializeAsync()
        {
            InitCallCount++;

            if (!_failedOnce)
            {
                _failedOnce = true;
                throw new InvalidOperationException("fail once");
            }

            return Task.CompletedTask;
        }
    }
    
    private sealed class CancelOnceLayoutContext : LayoutContextBase
    {
        private bool _canceledOnce;
        public int InitCallCount { get; private set; }

        protected override Task OnInitializeAsync()
        {
            InitCallCount++;

            if (!_canceledOnce)
            {
                _canceledOnce = true;
                return Task.FromCanceled(new CancellationToken(true)); // => IsCanceled
            }

            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task InitializeAsync_WhenInitThrows_DoesNotMarkInitialized_And_AllowsRetry()
    {
        var ctx = new AlwaysFailLayoutContext();

        var t1 = ctx.InitializeAsync();
        await Assert.ThrowsAsync<InvalidOperationException>(() => t1);

        // after failure, a new call must try again (=> second attempt)
        var t2 = ctx.InitializeAsync();
        await Assert.ThrowsAsync<InvalidOperationException>(() => t2);

        Assert.Equal(2, ctx.InitCallCount);
        Assert.NotSame(t1, t2);
    }

    [Fact]
    public async Task InitializeAsync_AfterFailure_AllowsRetry_AndEventuallySucceeds()
    {
        var ctx = new FailOnceLayoutContext();

        // first attempt fails
        await Assert.ThrowsAsync<InvalidOperationException>(() => ctx.InitializeAsync());
        Assert.Equal(1, ctx.InitCallCount);

        // second attempt succeeds
        await ctx.InitializeAsync();
        Assert.Equal(2, ctx.InitCallCount);

        // subsequent calls are idempotent
        await ctx.InitializeAsync();
        Assert.Equal(2, ctx.InitCallCount);
    }
    
    [Fact]
    public async Task InitializeAsync_WhenInitIsCanceled_AllowsRetry_AndEventuallySucceeds()
    {
        var ctx = new CancelOnceLayoutContext();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => ctx.InitializeAsync());
        Assert.Equal(1, ctx.InitCallCount);

        await ctx.InitializeAsync();
        Assert.Equal(2, ctx.InitCallCount);

        await ctx.InitializeAsync();
        Assert.Equal(2, ctx.InitCallCount);
    }
}