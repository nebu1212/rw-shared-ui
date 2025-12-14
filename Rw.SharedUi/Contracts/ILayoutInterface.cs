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
}