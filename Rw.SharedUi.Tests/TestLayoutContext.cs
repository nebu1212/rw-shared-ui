using Rw.SharedUi.Contracts;
using Rw.SharedUi.Layout;

namespace Rw.SharedUi.Tests;

internal sealed class TestLayoutContext : LayoutContextBase
{
    private int _initCallCount;
    public int InitCallCount => Volatile.Read(ref _initCallCount);

    protected override Task OnInitializeAsync()
    {
        Interlocked.Increment(ref _initCallCount);
        return Task.CompletedTask;
    }

    // Expose protected setters for testing
    public void TestSetHeader(string title, string? subtitle = null) => SetHeader(title, subtitle);
    public void TestSetNavigation(IEnumerable<NavbarItem> items) => SetNavigation(items);
    public void TestSetFooter(string? l, string? c, string? r) => SetFooter(l, c, r);
    public void TestSetProfile(string? name, string? img) => SetProfile(name, img);
    public void TestSetProfileMenu(IEnumerable<ProfileMenuItem> items) => SetProfileMenu(items);
}