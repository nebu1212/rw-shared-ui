using Rw.SharedUi.Contracts;
using Rw.SharedUi.Tests.Contracts;

namespace Rw.SharedUi.Tests.Layout;

public class LayoutContextBaseTest
{
    private sealed class TestLayoutContext : LayoutContextBase
    {
        // export protected methods as public for testing
        public void SetHeaderPublic(string title, string? subtitle = null)
            => SetHeader(title, subtitle);

        public void SetNavigationPublic(IEnumerable<NavbarItem> items)
            => SetNavigation(items);

        public void SetFooterPublic(string? left, string? center, string? right)
            => SetFooter(left, center, right);

        public void SetProfilePublic(string? name, string? image)
            => SetProfile(name, image);

        public void SetThemePublic(ThemeMode mode)
            => SetThemeMode(mode);

        public void SetProfileMenuPublic(IEnumerable<ProfileMenuItem> items)
            => SetProfileMenu(items);
    }

    [Fact]
    public void LayoutContextBase_CanBeConstructed_DefaultValues()
    {
        // Arrange & Act
        var context = new TestLayoutContext();

        // Assert
        Assert.Equal("App", context.AppTitle);
        Assert.Null(context.AppSubtitle);

        Assert.NotNull(context.NavbarItems);
        Assert.Empty(context.NavbarItems);
        Assert.True(context.IsSidebarOpen);

        Assert.Null(context.FooterLeft);
        Assert.Null(context.FooterCenter);
        Assert.Null(context.FooterRight);

        Assert.Null(context.DisplayName);
        Assert.Null(context.ProfileImageUrl);

        Assert.NotNull(context.ProfileMenuItems);
        Assert.Empty(context.ProfileMenuItems);

        Assert.Equal(ThemeMode.Dark, context.ThemeMode);
    }
    
    [Fact]
    public void SetHeader_SetsTitleAndSubtitle_AndRaisesChanged()
    {
        // Arrange
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;
        
        // Act & Assert
        ctx.SetHeaderPublic("TestApp", "Sub");

        Assert.Equal("TestApp", ctx.AppTitle);
        Assert.Equal("Sub", ctx.AppSubtitle);
        Assert.Equal(1, changedCount);

        ctx.SetHeaderPublic("New", null);

        Assert.Equal("New", ctx.AppTitle);
        Assert.Null(ctx.AppSubtitle);
        Assert.Equal(2, changedCount);
    }
    
    [Fact]
    public void SetNavigation_SetsItems_AndRaisesChanged()
    {
        // Arrange
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        var items = new[]
        {
            new NavbarItem("home", "Home", "/"),
            new NavbarItem("settings", "Einstellungen", "/settings")
        };
        
        // Act
        ctx.SetNavigationPublic(items);

        // Assert
        Assert.Equal(2, ctx.NavbarItems.Count);
        Assert.Equal("home", ctx.NavbarItems[0].Id);
        Assert.Equal(1, changedCount);
    }

    [Fact]
    public void SetNavigation_ThrowsOnNull()
    {
        // Arrange
        var ctx = new TestLayoutContext();
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            ctx.SetNavigationPublic(null!));
    }
    
    [Fact]
    public void ToggleSidebar_TogglesState_AndRaisesChanged()
    {
        
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        Assert.True(ctx.IsSidebarOpen);

        ctx.ToggleSidebar();
        Assert.False(ctx.IsSidebarOpen);
        Assert.Equal(1, changedCount);

        ctx.ToggleSidebar();
        Assert.True(ctx.IsSidebarOpen);
        Assert.Equal(2, changedCount);
    }
    
    [Fact]
    public void SetFooter_SetsTexts_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        ctx.SetFooterPublic("L", "C", "R");

        Assert.Equal("L", ctx.FooterLeft);
        Assert.Equal("C", ctx.FooterCenter);
        Assert.Equal("R", ctx.FooterRight);
        Assert.Equal(1, changedCount);

        ctx.SetFooterPublic(null, "OnlyCenter", null);

        Assert.Null(ctx.FooterLeft);
        Assert.Equal("OnlyCenter", ctx.FooterCenter);
        Assert.Null(ctx.FooterRight);
        Assert.Equal(2, changedCount);
    }
    
    [Fact]
    public void ToggleTheme_TogglesMode_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        Assert.Equal(ThemeMode.Dark, ctx.ThemeMode);

        ctx.SetThemeMode(ThemeMode.Light);
        Assert.Equal(ThemeMode.Light, ctx.ThemeMode);
        Assert.Equal(1, changedCount);

        ctx.SetThemeMode(ThemeMode.Dark);
        Assert.Equal(ThemeMode.Dark, ctx.ThemeMode);
        Assert.Equal(2, changedCount);
        
        ctx.SetThemeMode(ThemeMode.System);
        Assert.Equal(ThemeMode.System, ctx.ThemeMode);
        Assert.Equal(3, changedCount);
    }

    [Fact]
    public void SetTheme_SetsMode_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        ctx.SetThemePublic(ThemeMode.Light);

        Assert.Equal(ThemeMode.Light, ctx.ThemeMode);
        Assert.Equal(1, changedCount);
    }

    [Fact]
    public void SetProfile_SetsNameAndImage_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        ctx.SetProfilePublic("Peter", null);

        Assert.Equal("Peter", ctx.DisplayName);
        Assert.Null(ctx.ProfileImageUrl);
        Assert.Equal(1, changedCount);

        ctx.SetProfilePublic("Peter", "data:image/png;base64,XXX");

        Assert.Equal("Peter", ctx.DisplayName);
        Assert.Equal("data:image/png;base64,XXX", ctx.ProfileImageUrl);
        Assert.Equal(2, changedCount);
    }

    [Fact]
    public void SetProfileMenu_SetsItems_AndRaisesChanged()
    {
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        var items = new[]
        {
            new ProfileMenuItem("profile", "Profil", "account_circle"),
            new ProfileMenuItem("logout", "Logout", "logout")
        };

        ctx.SetProfileMenuPublic(items);

        Assert.Equal(2, ctx.ProfileMenuItems.Count);
        Assert.Equal("profile", ctx.ProfileMenuItems[0].Id);
        Assert.Equal(1, changedCount);
    }

    [Fact]
    public void SetProfileMenu_ThrowsOnNull()
    {
        var ctx = new TestLayoutContext();

        Assert.Throws<ArgumentNullException>(() =>
            ctx.SetProfileMenuPublic(null!));
    }

    [Fact]
    public async Task Default_OnProfileMenuItemClicked_DoesNotThrow()
    {
        var ctx = new TestLayoutContext();
        var item = new ProfileMenuItem("logout", "Logout", "logout");

        // is just checking that no exception is thrown
        await ctx.OnProfileMenuItemClickedAsync(item);
    }

    [Fact]
    public void MultipleOperations_RaiseChangedEachTime()
    {
        var ctx = new TestLayoutContext();
        int changedCount = 0;
        ctx.Changed += () => changedCount++;

        ctx.SetHeaderPublic("A");
        ctx.SetFooterPublic("L", null, null);
        ctx.SetProfilePublic("User", null);
        ctx.SetNavigationPublic([new NavbarItem("home", "Home", "/")]);

        Assert.Equal(4, changedCount);
    }
}