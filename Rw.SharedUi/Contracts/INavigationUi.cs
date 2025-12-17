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

    /// <summary>
    /// Gets or sets a value indicating whether the sidebar is open.
    /// </summary>
    bool IsSidebarOpen { get; }
    
    /// <summary>
    /// Sets the sidebar open or closed and raises change notifications.
    /// </summary>
    /// <param name="open"></param>
    void SetSidebarOpen(bool open);

    /// <summary>
    /// Toggles the sidebar open or closed and raises change notifications.
    /// </summary>
    void ToggleSidebar();
}