using MudBlazor;
using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Tests.TestData;

public class ProfileMenuTestData
{
    public static IReadOnlyList<ProfileMenuItem> Create() =>
    [
        new ProfileMenuItem("1", "Profile",  Icons.Material.Filled.Person,   1),
        new ProfileMenuItem("2", "Settings", Icons.Material.Filled.Settings, 2),
        new ProfileMenuItem("3", "Logout",   Icons.Material.Filled.Logout,   3),
    ];

    public static ProfileMenuItem CreateSingle() =>
        new ProfileMenuItem("4", "Testcase", Icons.Material.Filled.Tablet, 4);
}