namespace Rw.SharedUi.Contracts;

public interface IThemeUi
{
    /// <summary>
    /// Gets the current theme mode.
    /// </summary>
    ThemeMode ThemeMode { get; }

    /// <summary>
    /// Sets the theme mode.
    /// </summary>
    /// <param name="mode">The theme mode to set.</param>
    void SetThemeMode(ThemeMode mode);

    /// <summary>
    /// Toggles the theme mode asynchronously.
    /// </summary>
    /// <returns></returns>
    Task ToggleThemeAsync();
}