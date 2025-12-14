namespace Rw.SharedUi.Contracts;

/// <summary>
/// Represents the available theme modes supported by the UI.
/// </summary>
public enum ThemeMode
{
    /// <summary>
    /// Follows the operating system preference.
    /// </summary>
    System = 0,
    
    /// <summary>
    /// Forces the light theme.
    /// </summary>
    Light = 1,
    
    /// <summary>
    /// Forces the dark theme.
    /// </summary>
    Dark = 2
}