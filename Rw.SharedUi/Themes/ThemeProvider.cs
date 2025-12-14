using MudBlazor;

namespace Rw.SharedUi.Themes;

/// <summary>
/// Supplies the shared MudBlazor theme configuration for the UI library.
/// </summary>
public static class ThemeProvider
{
    /// <summary>
    /// Gets the application-wide theme definition used by MudBlazor components.
    /// </summary>
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