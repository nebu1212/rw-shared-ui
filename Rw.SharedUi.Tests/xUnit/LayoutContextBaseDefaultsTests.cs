using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseDefaultsTests
{
    [Fact]
    public void Defaults_Header_AreAsExpected()
    {
        var ctx = new TestLayoutContext();

        Assert.Equal("App", ctx.AppTitle);
        Assert.Null(ctx.AppSubtitle);
    }

    [Fact]
    public void Defaults_Navigation_AreAsExpected()
    {
        var ctx = new TestLayoutContext();

        Assert.True(ctx.IsSidebarOpen);

        Assert.NotNull(ctx.NavbarItems);
        Assert.Empty(ctx.NavbarItems);
        Assert.False(ctx.NavbarItems is List<NavbarItem>); // should not be a mutable list
    }

    [Fact]
    public void Defaults_Footer_AreAsExpected()
    {
        var ctx = new TestLayoutContext();

        Assert.Null(ctx.FooterLeft);
        Assert.Null(ctx.FooterCenter);
        Assert.Null(ctx.FooterRight);
    }

    [Fact]
    public void Defaults_Theme_AreAsExpected()
    {
        var ctx = new TestLayoutContext();

        Assert.Equal(ThemeMode.Dark, ctx.ThemeMode);
    }

    [Fact]
    public void Defaults_Profile_AreAsExpected()
    {
        var ctx = new TestLayoutContext();

        Assert.Null(ctx.DisplayName);
        Assert.Null(ctx.ProfileImageUrl);

        Assert.NotNull(ctx.ProfileMenuItems);
        Assert.Empty(ctx.ProfileMenuItems);
        Assert.False(ctx.ProfileMenuItems is List<ProfileMenuItem>);
    }
}