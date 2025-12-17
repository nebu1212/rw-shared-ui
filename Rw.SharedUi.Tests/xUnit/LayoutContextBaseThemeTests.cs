using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class LayoutContextBaseThemeTests
{
    [Fact]
    public void SetThemeMode_SameValue_NoEvents()
    {
        var ctx = new TestLayoutContext();
        var changed = 0;
        var themeChanged = 0;

        ctx.Changed += () => changed++;
        ctx.ThemeModeChanged += _ => themeChanged++;

        ctx.SetThemeMode(ctx.ThemeMode);

        Assert.Equal(0, changed);
        Assert.Equal(0, themeChanged);
    }

    [Fact]
    public void SetThemeMode_NewValue_RaisesThemeModeChanged_BeforeChanged()
    {
        var ctx = new TestLayoutContext();
        var order = new List<string>();

        ctx.ThemeModeChanged += _ => order.Add("ThemeModeChanged");
        ctx.Changed += () => order.Add("Changed");

        ctx.SetThemeMode(ThemeMode.Light);

        Assert.Equal(ThemeMode.Light, ctx.ThemeMode);
        Assert.Equal(new[] { "ThemeModeChanged", "Changed" }, order);
    }

    [Fact]
    public async Task ToggleThemeAsync_Cycles_Dark_System_Light_Dark_AndRaisesEventsEachTime()
    {
        var ctx = new TestLayoutContext();

        var changed = 0;
        var themeChanged = 0;

        ctx.Changed += () => changed++;
        ctx.ThemeModeChanged += _ => themeChanged++;

        // Default: Dark -> System
        await ctx.ToggleThemeAsync();
        Assert.Equal(ThemeMode.System, ctx.ThemeMode);

        // System -> Light
        await ctx.ToggleThemeAsync();
        Assert.Equal(ThemeMode.Light, ctx.ThemeMode);

        // Light -> Dark
        await ctx.ToggleThemeAsync();
        Assert.Equal(ThemeMode.Dark, ctx.ThemeMode);

        Assert.Equal(3, changed);
        Assert.Equal(3, themeChanged);
    }

    [Fact]
    public async Task ToggleThemeAsync_FromSystem_GoesToLight()
    {
        var ctx = new TestLayoutContext();
        ctx.SetThemeMode(ThemeMode.System);

        await ctx.ToggleThemeAsync();

        Assert.Equal(ThemeMode.Light, ctx.ThemeMode);
    }

    [Fact]
    public async Task ToggleThemeAsync_FromLight_GoesToDark()
    {
        var ctx = new TestLayoutContext();
        ctx.SetThemeMode(ThemeMode.Light);

        await ctx.ToggleThemeAsync();

        Assert.Equal(ThemeMode.Dark, ctx.ThemeMode);
    }
}