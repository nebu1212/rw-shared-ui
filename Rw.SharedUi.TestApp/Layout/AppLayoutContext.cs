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
            // Root
            new NavbarItem(
                id: "dashboard",
                text: "Dashboard",
                href: "/",
                icon: Icons.Material.Filled.Dashboard,
                order: 0
            ),

            // Users (Group)
            new(
                id: "users",
                text: "Users",
                href: null,
                icon: Icons.Material.Filled.People,
                order: 10
            ),

            new(
                id: "users-list",
                text: "User List",
                href: "/users",
                icon: Icons.Material.Filled.List,
                parentId: "users",
                order: 0
            ),

            new(
                id: "users-create",
                text: "Create User",
                href: "/users/create",
                icon: Icons.Material.Filled.PersonAdd,
                parentId: "users",
                order: 10
            ),

            // User Details (3. Ebene, nur logisch – kein eigener Menüpunkt)
            new(
                id: "users-details",
                text: "User Details",
                href: "/users/details",
                icon: Icons.Material.Filled.Person,
                parentId: "users",
                order: 20
            ),

            new(
                id: "users-details-edit",
                text: "Edit User",
                href: "/users/details/edit",
                icon: Icons.Material.Filled.Edit,
                parentId: "users-details",
                order: 0
            ),

            // Settings (Group)
            new(
                id: "settings",
                text: "Settings",
                href: null,
                icon: Icons.Material.Filled.Settings,
                order: 20
            ),

            new(
                id: "settings-general",
                text: "General",
                href: "/settings/general",
                icon: Icons.Material.Filled.Tune,
                parentId: "settings",
                order: 0
            ),

            new(
                id: "settings-security",
                text: "Security",
                href: "/settings/security",
                icon: Icons.Material.Filled.Security,
                parentId: "settings",
                order: 10
            ),

            new(
                id: "settings-security-password",
                text: "Password",
                href: "/settings/security/password",
                icon: Icons.Material.Filled.Password,
                parentId: "settings-security",
                order: 0
            )
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