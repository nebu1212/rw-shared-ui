using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Layout;

/// <summary>
/// Provides a base implementation of <see cref="ILayoutContext"/> that manages shared UI state.
/// </summary>
public abstract class LayoutContextBase : ILayoutContext
{
    
    /// <summary>
    /// Occurs when any layout property changes.
    /// </summary>
    public event Action? Changed;
    
    /// <summary>
    /// Raises the <see cref="Changed"/> event to notify listeners of state updates.
    /// </summary>
    private void RaiseChanged() => Changed?.Invoke();
    
    
    /// <summary>
    /// Gets the title displayed in the layout header.
    /// </summary>
    public string AppTitle { get; private set; } = "App";
    
    /// <summary>
    /// Gets the optional subtitle displayed in the layout header.
    /// </summary>
    public string? AppSubtitle { get; private set; }
    
    /// <summary>
    /// Updates the header information and raises change notifications.
    /// </summary>
    /// <param name="title">The title to display.</param>
    /// <param name="subtitle">The optional subtitle to display.</param>
    protected void SetHeader(string title, string? subtitle = null)
    {
        this.AppTitle = title ?? throw new ArgumentNullException(nameof(title));
        this.AppSubtitle = subtitle;
        RaiseChanged();
    }


    /// <summary>
    /// Backing field for navigation items to preserve immutability.
    /// </summary>
    public IReadOnlyList<NavbarItem> NavbarItems { get; private set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether the sidebar is currently open.
    /// </summary>
    public bool IsSidebarOpen { get; set; } = true;
    
    /// <summary>
    /// Toggles the sidebar visibility and notifies listeners.
    /// </summary>
    public void ToggleSidebar()
    {
        this.IsSidebarOpen = !this.IsSidebarOpen;
        RaiseChanged();
    }
    
    /// <summary>
    /// Replaces the navigation items and raises change notifications.
    /// </summary>
    /// <param name="items">The navigation items to apply.</param>
    protected void SetNavigation(IEnumerable<NavbarItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }
        
        this.NavbarItems = items.ToList().AsReadOnly();
        RaiseChanged();
    }
    
    
    /// <summary>
    /// Gets the text displayed on the left side of the footer.
    /// </summary>
    public string? FooterLeft { get; private set; }
    
    /// <summary>
    /// Gets the text displayed at the center of the footer.
    /// </summary>
    public string? FooterCenter { get; private set; }
    
    /// <summary>
    /// Gets the text displayed on the right side of the footer.
    /// </summary>
    public string? FooterRight { get; private set; }
    
    /// <summary>
    /// Sets the footer content and raises change notifications.
    /// </summary>
    /// <param name="left">The left footer text.</param>
    /// <param name="center">The center footer text.</param>
    /// <param name="right">The right footer text.</param>
    protected void SetFooter(string? left, string? center, string? right)
    {
        this.FooterLeft = left;
        this.FooterCenter = center;
        this.FooterRight = right;
        RaiseChanged();
    }
    
    
    /// <summary>
    /// Gets the current theme mode applied to the layout.
    /// </summary>
    public ThemeMode ThemeMode { get; private set; } = ThemeMode.Dark;
    
    /// <summary>
    /// Occurs when the theme mode changes.
    /// </summary>
    public event Action<ThemeMode>? ThemeModeChanged;
    
    /// <summary>
    /// Sets a new theme mode and notifies listeners if it changed.
    /// </summary>
    /// <param name="mode">The theme mode to apply.</param>
    public void SetThemeMode(ThemeMode mode)
    {
        if (this.ThemeMode == mode)
        {
            return;
        }
        this.ThemeMode = mode;
        ThemeModeChanged?.Invoke(mode);
        RaiseChanged();
    }

    /// <summary>
    /// Cycles through the available theme modes and applies the next value.
    /// </summary>
    /// <returns>A completed task when the toggle operation finishes.</returns>
    public Task ToggleThemeAsync()
    {
        var newMode = ThemeMode switch
        {
            ThemeMode.System => ThemeMode.Light,
            ThemeMode.Light  => ThemeMode.Dark,
            _                => ThemeMode.System
        };
        
        SetThemeMode(newMode);
        return Task.CompletedTask;
    }


    /// <summary>
    /// Gets the display name for the active user.
    /// </summary>
    public string? DisplayName { get; private set; }
    
    /// <summary>
    /// Gets the URL of the active user's profile image.
    /// </summary>
    public string? ProfileImageUrl { get; private set; }
    
    /// <summary>
    /// Updates the profile information and raises change notifications.
    /// </summary>
    /// <param name="name">The user's display name.</param>
    /// <param name="image">The user's profile image URL.</param>
    protected void SetProfile(string? name, string? image)
    {
        this.DisplayName = name;
        this.ProfileImageUrl = image;
        RaiseChanged();
    }


    /// <summary>
    /// Backing storage for the profile menu items to enforce immutability.
    /// </summary>
    public IReadOnlyList<ProfileMenuItem> ProfileMenuItems { get; private set; } = [];

    /// <summary>
    /// Sets the profile menu entries and raises change notifications.
    /// </summary>
    /// <param name="items">The menu items to display.</param>
    protected void SetProfileMenu(IEnumerable<ProfileMenuItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }
        
        this.ProfileMenuItems = items.ToList().AsReadOnly();
        RaiseChanged();
    }
    
    /// <summary>
    /// Handles clicks on profile menu entries. The base implementation is a no-op.
    /// </summary>
    /// <param name="item">The clicked profile menu item.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual Task OnProfileMenuItemClickedAsync(ProfileMenuItem item)
    {
        // Default: do nothing
        return Task.CompletedTask;
    }
}