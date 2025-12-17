using MudBlazor;
using Rw.SharedUi.Themes;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class DataThemeProviderTests
{
    [Fact]
    public void Theme_IsNotNull()
    {
        Assert.NotNull(ThemeProvider.Theme);
    }

    [Fact]
    public void Theme_IsSingleton_InstanceDoesNotChange()
    {
        var a = ThemeProvider.Theme;
        var b = ThemeProvider.Theme;

        Assert.Same(a, b);
    }

    [Fact]
    public void Theme_HasRequiredSubObjects()
    {
        MudTheme theme = ThemeProvider.Theme;

        Assert.NotNull(theme.PaletteLight);
        Assert.NotNull(theme.PaletteDark);
        Assert.NotNull(theme.Typography);
        Assert.NotNull(theme.Typography.Default);
    }
}