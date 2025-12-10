namespace Rw.SharedUi.Contracts;

public interface ILayoutContext : IHeaderUi, INavigationUi, IFooterUi, IProfileUi, IProfileMenuUi, IThemeUi
{
    /// <summary>
    /// Event triggered when any layout property changes.
    /// </summary>
    event Action? Changed;
}