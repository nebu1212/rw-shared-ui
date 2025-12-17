using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class DataProfileMenuItemTests
{
    [Fact]
    public void Constructor_MinimalValues_SetsDefaults()
    {
        var item = new ProfileMenuItem("id", "Settings");

        Assert.Equal("id", item.Id);
        Assert.Equal("Settings", item.Text);
        Assert.Null(item.Icon);
        Assert.Equal(0, item.Order);
    }

    [Fact]
    public void Constructor_AllValues_AreAssigned()
    {
        var item = new ProfileMenuItem("help", "Help", "help-icon", 5);

        Assert.Equal("help", item.Id);
        Assert.Equal("Help", item.Text);
        Assert.Equal("help-icon", item.Icon);
        Assert.Equal(5, item.Order);
    }

    [Fact]
    public void Constructor_Allows_NullIcon()
    {
        var item = new ProfileMenuItem("id", "Text", icon: null, order: 1);

        Assert.Null(item.Icon);
        Assert.Equal(1, item.Order);
    }

    [Fact]
    public void Constructor_Allows_ZeroAndNegativeOrder()
    {
        var zero = new ProfileMenuItem("id1", "A", order: 0);
        var negative = new ProfileMenuItem("id2", "B", order: -10);

        Assert.Equal(0, zero.Order);
        Assert.Equal(-10, negative.Order);
    }

    [Fact]
    public void ProfileMenuItem_Properties_AreReadOnly()
    {
        var props = typeof(ProfileMenuItem).GetProperties();

        Assert.All(props, p =>
            Assert.False(p.CanWrite, $"Property {p.Name} should be read-only"));
    }
    
    [Fact]
    public void Ctor_Allows_NullIcon_And_DefaultOrder()
    {
        var item = new ProfileMenuItem("id", "text");

        Assert.Equal("id", item.Id);
        Assert.Equal("text", item.Text);
        Assert.Null(item.Icon);
        Assert.Equal(0, item.Order);
    }

    [Fact]
    public void Ctor_NullId_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ProfileMenuItem(null!, "text"));
    }

    [Fact]
    public void Ctor_NullText_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ProfileMenuItem("id", null!));
    }
}