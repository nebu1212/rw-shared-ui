namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseProfileTests
{
    [Fact]
    public void SetProfile_RaisesChanged_ExactlyOnce_AndSetsValues()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetProfile("User", "img");

        Assert.Equal(1, changed);
        Assert.Equal("User", ctx.DisplayName);
        Assert.Equal("img", ctx.ProfileImageUrl);
    }

    [Fact]
    public void SetProfile_AllowsNulls_SetsNulls_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetProfile(null, null);

        Assert.Equal(1, changed);
        Assert.Null(ctx.DisplayName);
        Assert.Null(ctx.ProfileImageUrl);
    }

    [Fact]
    public void SetProfile_CanBeCalledMultipleTimes_RaisesChangedEachTime()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetProfile("A", "1");
        ctx.TestSetProfile("B", "2");

        Assert.Equal(2, changed);
        Assert.Equal("B", ctx.DisplayName);
        Assert.Equal("2", ctx.ProfileImageUrl);
    }
}