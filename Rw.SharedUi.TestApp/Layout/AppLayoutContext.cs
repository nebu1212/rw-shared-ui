using MudBlazor;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.Layout;
using Rw.SharedUi.TestApp.Layout.TestData;

namespace Rw.SharedUi.TestApp.Layout;

/// <summary>
/// Test application layout context used to visually validate SharedUi behavior.
/// </summary>
public sealed class AppLayoutContext : LayoutContextBase
{
    private readonly IThemeModeStore _themeStore;
    private readonly ISidebarStateStore _sidebarStore;
    
    private bool _persistenceEnabled;

    public AppLayoutContext(IThemeModeStore themeStore, ISidebarStateStore sidebarStore)
    {
        this._themeStore = themeStore;
        this._sidebarStore = sidebarStore;
    }

    /// <summary>
    /// Initializes the context once. This method is called via LayoutContextBase.InitializeAsync().
    /// </summary>
    protected override async Task OnInitializeAsync()
    {
        // Header
        SetHeader(LayoutTextTestData.AppTitle, LayoutTextTestData.AppSubtitle);

        // Navigation
        SetNavigation(NavbarTestData.Create());

        // Footer
        SetFooter(LayoutTextTestData.FooterLeft, LayoutTextTestData.FooterCenter, LayoutTextTestData.FooterRight);

        // Profile
        SetProfile("Test user", "https://example.com/profile.jpg");
        SetProfileMenu(ProfileMenuTestData.Create());

        // Load persisted UI preferences
        this._persistenceEnabled = false;
        
        ThemeMode? savedTheme = null;
        try { savedTheme = await this._themeStore.LoadAsync(); } catch { /* ignore */ }

        if (savedTheme is not null)
        {
            base.SetThemeMode(savedTheme.Value);    
        }
        else
        {
            const ThemeMode defaultTheme = ThemeMode.Dark;       
            base.SetThemeMode(defaultTheme); 
            try { await this._themeStore.SaveAsync(defaultTheme); } catch { /* ignore */ } // write once
        }

        bool? savedSidebar = null;
        try { savedSidebar = await this._sidebarStore.LoadAsync(); } catch { /* ignore */ }

        if (savedSidebar is not null)
        {
            base.SetSidebarOpen(savedSidebar.Value);             
        }
        else
        {
            const bool defaultSidebar = true;
            base.SetSidebarOpen(defaultSidebar);  
            try { await this._sidebarStore.SaveAsync(defaultSidebar); } catch { /* ignore */ } // write once
        }

        this._persistenceEnabled = true;
    }
    
    public override void SetThemeMode(ThemeMode mode)
    {
        base.SetThemeMode(mode);

        if (_persistenceEnabled)
            _ = PersistThemeAsync(mode);
    }

    public override void SetSidebarOpen(bool open)
    {
        base.SetSidebarOpen(open);

        if (_persistenceEnabled)
            _ = PersistSidebarAsync(open);
    }

    private async Task PersistThemeAsync(ThemeMode mode)
    {
        try { await _themeStore.SaveAsync(mode); } catch { /* ignore */ }
    }

    private async Task PersistSidebarAsync(bool open)
    {
        try { await _sidebarStore.SaveAsync(open); } catch { /* ignore */ }
    }
    
}