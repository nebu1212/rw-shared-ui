namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines the UI elements for a footer component.
/// </summary>
public interface IFooterUi
{
    /// <summary>
    /// Gets the left footer content.
    /// </summary>
    string? FooterLeft { get; }

    /// <summary>
    /// Gets the center footer content.
    /// </summary>
    string? FooterCenter { get; }
    
    /// <summary>
    /// Gets the right footer content.
    /// </summary>
    string? FooterRight { get; }
}