namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines the UI elements for navigation components.
/// </summary>
public interface INavigationUi
{
    /// <summary>
    /// Gets the list of navbar items.
    /// </summary>
    IReadOnlyList<NavbarItem> NavbarItems { get; }

    /// <summary>>
    /// Gets a value indicating whether the sidebar is open.
    /// </summary>
    bool IsSidebarOpen { get; }

    /// <summary>
    /// Toggles the sidebar open or closed.
    /// </summary>
    void ToggleSidebar();
}