using Rw.SharedUi.Contracts;
using System;
using System.Linq;

namespace Rw.SharedUi.Tests.Contracts;

public abstract class LayoutContextBase : ILayoutContext
{
    // Event
    public event Action? Changed;
    protected void RaiseChanged() => Changed?.Invoke();
    
    // Header -- HeaderUi
    public string AppTitle { get; private set; } = "App";
    public string? AppSubtitle { get; private set; }
    
    protected void SetHeader(string title, string? subtitle = null)
    {
        if (title is null)
        {
            throw new ArgumentNullException(nameof(title));
        }
        
        this.AppTitle = title;
        this.AppSubtitle = subtitle;
        RaiseChanged();
    }
    
    // Navigation / Sidebar -- NavigationUi
    private IReadOnlyList<NavbarItem> _navItems = [];
    public IReadOnlyList<NavbarItem> NavbarItems => _navItems;
    public bool IsSidebarOpen { get; private set; } = true;
    
    public void ToggleSidebar()
    {
        this.IsSidebarOpen = !this.IsSidebarOpen;
        RaiseChanged();
    }
    
    protected void SetNavigation(IEnumerable<NavbarItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }
        
        this._navItems = items.ToList().AsReadOnly();
        RaiseChanged();
    }
    
    // Footer -- FooterUi
    public string? FooterLeft { get; private set; }
    public string? FooterCenter { get; private set; }
    public string? FooterRight { get; private set; }
    
    protected void SetFooter(string? left, string? center, string? right)
    {
        this.FooterLeft = left;
        this.FooterCenter = center;
        this.FooterRight = right;
        RaiseChanged();
    }
    
    // Theme -- ThemeUi
    public ThemeMode ThemeMode { get; private set; } = ThemeMode.Dark;
    
    public void SetThemeMode(ThemeMode mode)
    {
        this.ThemeMode = mode;
        RaiseChanged();
    }
    
    
    // Profile -- ProfileUi
    public string? DisplayName { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    
    protected void SetProfile(string? name, string? image)
    {
        this.DisplayName = name;
        this.ProfileImageUrl = image;
        RaiseChanged();
    }
    
    // Profile Menu -- ProfileMenuUi
    private IReadOnlyList<ProfileMenuItem> _profileMenuItems = [];
    public IReadOnlyList<ProfileMenuItem> ProfileMenuItems => _profileMenuItems;
    
    protected void SetProfileMenu(IEnumerable<ProfileMenuItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }
        
        this._profileMenuItems = items.ToList().AsReadOnly();
        RaiseChanged();
    }
    
    public virtual Task OnProfileMenuItemClickedAsync(ProfileMenuItem item)
    {
        // Default: do nothing
        return Task.CompletedTask;
    }
}