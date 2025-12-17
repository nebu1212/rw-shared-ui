namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseFooterTests
{
    [Fact]
    public void SetFooter_RaisesChanged_ExactlyOnce_AndSetsValues()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetFooter("L", "C", "R");

        Assert.Equal(1, changed);
        Assert.Equal("L", ctx.FooterLeft);
        Assert.Equal("C", ctx.FooterCenter);
        Assert.Equal("R", ctx.FooterRight);
    }

    [Fact]
    public void SetFooter_AllowsNulls_SetsNulls_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetFooter(null, null, null);

        Assert.Equal(1, changed);
        Assert.Null(ctx.FooterLeft);
        Assert.Null(ctx.FooterCenter);
        Assert.Null(ctx.FooterRight);
    }

    [Fact]
    public void SetFooter_CanBeCalledMultipleTimes_RaisesChangedEachTime()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        ctx.Changed += () => changed++;

        ctx.TestSetFooter("L1", "C1", "R1");
        ctx.TestSetFooter("L2", "C2", "R2");

        Assert.Equal(2, changed);
        Assert.Equal("L2", ctx.FooterLeft);
        Assert.Equal("C2", ctx.FooterCenter);
        Assert.Equal("R2", ctx.FooterRight);
    }
}