using MudBlazor;
using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.TestApp.Layout.TestData;

public class NavbarTestData
{
    public static IReadOnlyList<NavbarItem> Create() =>
    [
        // Root
        new NavbarItem(
            id: "dashboard",
            text: "Dashboard",
            href: "/",
            icon: Icons.Material.Filled.Dashboard,
            order: 0),

        // Users (Group)
        new NavbarItem(
            id: "users",
            text: "Users",
            href: null,
            icon: Icons.Material.Filled.People,
            order: 10),

        new NavbarItem(
            id: "users-list",
            text: "User List",
            href: "/users",
            icon: Icons.Material.Filled.List,
            parentId: "users",
            order: 0),

        new NavbarItem(
            id: "users-create",
            text: "Create User",
            href: "/users/create",
            icon: Icons.Material.Filled.PersonAdd,
            parentId: "users",
            order: 10),

        // Logical subtree anchor (can be clickable or not; keep it clickable for testing)
        new NavbarItem(
            id: "users-details",
            text: "User Details",
            href: "/users/details",
            icon: Icons.Material.Filled.Person,
            parentId: "users",
            order: 20),

        new NavbarItem(
            id: "users-details-edit",
            text: "Edit User",
            href: "/users/details/edit",
            icon: Icons.Material.Filled.Edit,
            parentId: "users-details",
            order: 0),

        // Settings (Group)
        new NavbarItem(
            id: "settings",
            text: "Settings",
            href: null,
            icon: Icons.Material.Filled.Settings,
            order: 20),

        new NavbarItem(
            id: "settings-general",
            text: "General",
            href: "/settings/general",
            icon: Icons.Material.Filled.Tune,
            parentId: "settings",
            order: 0),

        new NavbarItem(
            id: "settings-security",
            text: "Security",
            href: "/settings/security",
            icon: Icons.Material.Filled.Security,
            parentId: "settings",
            order: 10),

        new NavbarItem(
            id: "settings-security-password",
            text: "Password",
            href: "/settings/security/password",
            icon: Icons.Material.Filled.Password,
            parentId: "settings-security",
            order: 0),
    ];
}