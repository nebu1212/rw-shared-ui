using MudBlazor;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.Layout;

namespace Rw.SharedUi.TestApp.Layout;

public sealed class AppLayoutContext : LayoutContextBase
{
    public AppLayoutContext()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Header
        SetHeader("Test Application", "Demo of Shared UI Library");

        // Navigation
        SetNavigation(new[]
        {
            new NavbarItem("1","Home", "/", "home", null, 1),
            new NavbarItem("2","About", "/about", "info", null, 2),
            new NavbarItem("3","Contact", "/contact", "phone", null, 3),
            new NavbarItem("4","Email", "/contact/email", "email", "3", 4),
        });

        // Footer
        SetFooter(
            "© 2024 Test Application",
            "All rights reserved.",
            "Version 1.0.0"
        );
        
        // Theme
        SetThemeMode(ThemeMode.Dark);
        
        // Profile
        SetProfile("Test user", "https://example.com/profile.jpg");
        
        // Profile Menu
        SetProfileMenu(new[]
        {
            new ProfileMenuItem("1", "Profile", Icons.Material.Filled.Person, 1),
            new ProfileMenuItem("2", "Settings", Icons.Material.Filled.Settings, 2),
            new ProfileMenuItem("3", "Logout", Icons.Material.Filled.Logout, 3),
        });
    }
}