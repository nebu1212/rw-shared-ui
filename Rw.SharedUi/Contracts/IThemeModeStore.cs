namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines methods for persisting and retrieving the user's theme mode preference.
/// </summary>
public interface IThemeModeStore
{
    /// <summary>
    /// Loads the saved theme mode preference.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<ThemeMode?> LoadAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Saves the specified theme mode preference.
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task SaveAsync(ThemeMode mode, CancellationToken ct = default);
}