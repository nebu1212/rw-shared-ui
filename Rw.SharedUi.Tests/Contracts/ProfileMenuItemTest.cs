using Rw.SharedUi.Contracts;
using Xunit;

namespace Rw.SharedUi.Tests.Contracts;

public class ProfileMenuItemTest
{
    [Fact]
    public void ProfileMenuItem_CanBeConstructed_BaseValues()
    {
        // Arrange
        var id = "b32083a4-cf6a-4a78-a7e9-00bdea930d75";
        var text = "Settings";

        // Act
        var item = new ProfileMenuItem(id, text);

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(text, item.Text);
        Assert.Null(item.Icon);
        Assert.Equal(0, item.Order);
    }

    [Fact]
    public void ProfileMenuItem_CanBeConstructed_AllValues()
    {
        // Arrange
        var id = "b32083a4-cf6a-4a78-a7e9-00bdea930d75";
        var text = "Help";
        var icon = "help-icon";
        var order = 5;

        // Act
        var item = new ProfileMenuItem(id, text, icon, order);

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(text, item.Text);
        Assert.Equal(icon, item.Icon);
        Assert.Equal(order, item.Order);
    }
}