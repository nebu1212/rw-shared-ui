namespace Rw.SharedUi.Contracts;

/// <summary>
/// Provides theme-related state and behavior for the UI.
/// </summary>
public interface IThemeUi
{
    /// <summary>
    /// Gets the current theme mode.
    /// </summary>
    ThemeMode ThemeMode { get; }

    /// <summary>
    /// Raised when the theme mode has changed.
    /// </summary>
    event Action<ThemeMode>? ThemeModeChanged;
    
    /// <summary>
    /// Sets the theme mode.
    /// </summary>
    /// <param name="mode">The theme mode to set.</param>
    void SetThemeMode(ThemeMode mode);

    /// <summary>
    /// Toggles the theme mode asynchronously.
    /// </summary>
    /// <returns>A task that completes when the theme change has been applied.</returns>
    Task ToggleThemeAsync();
}