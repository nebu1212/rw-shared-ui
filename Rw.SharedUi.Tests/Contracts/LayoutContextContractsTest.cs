using Rw.SharedUi.Contracts;
using Xunit;

namespace Rw.SharedUi.Tests.Contracts;

public class LayoutContextContractsTest
{
    [Fact]
    public void LayoutContext_Implements_All_Contracts()
    {
        // Arrange
        ILayoutContext ctx = new LayoutContextImpl();


        // Act
        

        // Assert
        Assert.NotNull(ctx);
        Assert.IsAssignableFrom<IHeaderUi>(ctx);
        Assert.IsAssignableFrom<INavigationUi>(ctx);
        Assert.IsAssignableFrom<IFooterUi>(ctx);
        Assert.IsAssignableFrom<IProfileUi>(ctx);
        Assert.IsAssignableFrom<IProfileMenuUi>(ctx);
        Assert.IsAssignableFrom<IThemeUi>(ctx);
        
    }

    public class LayoutContextImpl : ILayoutContext
    {
        // --- IHeaderUi ---
        public string AppTitle { get; set; } = "Test";
        public string? AppSubtitle { get; set; }

        // --- INavigationUi ---
        public IReadOnlyList<NavbarItem> NavbarItems { get; set; } = [];
        public bool IsSidebarOpen { get; private set; }
        public void ToggleSidebar() => IsSidebarOpen = !IsSidebarOpen;

        // --- IFooterUi ---
        public string? FooterLeft { get; set; }
        public string? FooterCenter { get; set; }
        public string? FooterRight { get; set; }

        // --- IProfileUi ---
        public string? DisplayName { get; set; } = "Test User";
        public string? ProfileImageUrl { get; set; }

        // --- IProfileMenuUi ---
        public IReadOnlyList<ProfileMenuItem> ProfileMenuItems { get; set; }
            =
            [
                new ProfileMenuItem("profile", "Profil"),
                new ProfileMenuItem("settings", "Einstellungen"),
                new ProfileMenuItem("logout", "Logout")
            ];

        public Task OnProfileMenuItemClickedAsync(ProfileMenuItem item)
        {
            // Dummy: do nothing
            return Task.CompletedTask;
        }

        // --- IThemeUi ---
        public ThemeMode ThemeMode { get; private set; } = ThemeMode.Dark;

        public void SetThemeMode(ThemeMode mode)
        {
            ThemeMode = mode;
        }

        public Task ToggleThemeAsync()
        {
            ThemeMode = ThemeMode switch
            {
                ThemeMode.System => ThemeMode.Light,
                ThemeMode.Light  => ThemeMode.Dark,
                _                => ThemeMode.System
            };

            RaiseChanged();
            return Task.CompletedTask;
        }

        // --- ILayoutContext ---
        public event Action? Changed;
        private void RaiseChanged() => Changed?.Invoke();
    }
}