using Rw.SharedUi.Layout;

namespace Rw.SharedUi.Tests;

/// <summary>
/// Blocks initialization until Release() is called.
/// Used to test concurrency semantics deterministically.
/// </summary>
internal sealed class BlockingInitLayoutContext : LayoutContextBase
{
    private readonly TaskCompletionSource _entered =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    private readonly TaskCompletionSource _release =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    public int InitCallCount { get; private set; }

    public Task Entered => _entered.Task;

    public void Release() => _release.TrySetResult();

    protected override async Task OnInitializeAsync()
    {
        InitCallCount++;
        _entered.TrySetResult();
        await _release.Task;
    }
}