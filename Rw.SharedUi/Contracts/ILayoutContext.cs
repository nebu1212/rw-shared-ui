namespace Rw.SharedUi.Contracts;

/// <summary>
/// Aggregates the individual UI contract interfaces into a single layout context.
/// </summary>
public interface ILayoutContext : IHeaderUi, INavigationUi, IFooterUi, IProfileUi, IProfileMenuUi, IThemeUi
{
    /// <summary>
    /// Event triggered when any layout property changes.
    /// </summary>
    event Action? Changed;
    
    /// <summary>
    ///  Initializes the layout context asynchronously.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task InitializeAsync(CancellationToken ct = default);
}