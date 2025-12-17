namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseHeaderTests
{
    [Fact]
    public void SetHeader_RaisesChanged_ExactlyOnce_AndSetsValues()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetHeader("T", "S");

        Assert.Equal(1, changed);
        Assert.Equal("T", ctx.AppTitle);
        Assert.Equal("S", ctx.AppSubtitle);
    }

    [Fact]
    public void SetHeader_NullTitle_Throws_AndDoesNotRaiseChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        Assert.Throws<ArgumentNullException>(() => ctx.TestSetHeader(null!));

        Assert.Equal(0, changed);
        Assert.Equal("App", ctx.AppTitle); // default should remain unchanged
    }

    [Fact]
    public void SetHeader_NullSubtitle_IsAllowed_SetsNull_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetHeader("T", null);

        Assert.Equal(1, changed);
        Assert.Equal("T", ctx.AppTitle);
        Assert.Null(ctx.AppSubtitle);
    }

    [Fact]
    public void SetHeader_EmptyTitle_IsAllowed_SetsEmpty_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetHeader(string.Empty, "S");

        Assert.Equal(1, changed);
        Assert.Equal(string.Empty, ctx.AppTitle);
        Assert.Equal("S", ctx.AppSubtitle);
    }
}