namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines the UI elements for a header component.
/// </summary>
public interface IHeaderUi
{
    /// <summary>
    /// Gets the application title.
    /// </summary>
    string AppTitle { get; }

    /// <summary>
    /// Gets the application subtitle.
    /// </summary>
    string? AppSubtitle { get; }
}