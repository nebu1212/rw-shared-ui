using Rw.SharedUi.Contracts;
using Xunit;

namespace Rw.SharedUi.Tests.Contracts;

public class NavbarItemTest
{

    [Fact]
    public void NavbarItem_CanBeConstructed_BaseValues()
    {
        // Arrange
        var id = "68dd90bf-cd34-4455-a940-bcf4140a593d";
        var text = "Home";
        var href = "/";

        // Act
        var item = new NavbarItem(id, text, href);

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(text, item.Text);
        Assert.Equal(href, item.Href);
        Assert.Null(item.Icon);
        Assert.Null(item.ParentId);
        Assert.Equal(0, item.Order);
    }

    [Fact]
    public void NavbarItem_CanBeConstructed_AllValues()
    {
        // Arrange
        var id = "b32083a4-cf6a-4a78-a7e9-00bdea930d75";
        var text = "Profile";
        var href = "/profile";
        var icon = "user-icon";
        string? parentId = "80975b67-da7f-4c28-94a0-79d18be748e7";
        var order = 3;

        // Act
        var item = new NavbarItem(id, text, href, icon, parentId, order);

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(text, item.Text);
        Assert.Equal(href, item.Href);
        Assert.Equal(icon, item.Icon);
        Assert.Equal(parentId, item.ParentId);
        Assert.Equal(order, item.Order);
    }
}
