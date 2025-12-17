namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextSidebarTests
{
    [Fact]
    public void SetSidebarOpen_SameValue_DoesNotRaiseChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.SetSidebarOpen(true); // default true

        Assert.Equal(0, changed);
        Assert.True(ctx.IsSidebarOpen);
    }

    [Fact]
    public void SetSidebarOpen_NewValue_RaisesChanged_ExactlyOnce()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.SetSidebarOpen(false);

        Assert.Equal(1, changed);
        Assert.False(ctx.IsSidebarOpen);
    }

    [Fact]
    public void ToggleSidebar_TogglesAndRaisesChanged_ExactlyOncePerToggle()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        // default true -> false
        ctx.ToggleSidebar();
        Assert.False(ctx.IsSidebarOpen);

        // false -> true
        ctx.ToggleSidebar();
        Assert.True(ctx.IsSidebarOpen);

        Assert.Equal(2, changed);
    }
}