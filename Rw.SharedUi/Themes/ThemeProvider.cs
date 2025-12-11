using MudBlazor;

namespace Rw.SharedUi.Themes;

public static class ThemeProvider
{
    public static MudTheme Theme { get; } = new()
    {
        PaletteLight  = new(),
        Typography = new Typography()
        {
            Default = new DefaultTypography()
        },
        PaletteDark = new()
    };
}