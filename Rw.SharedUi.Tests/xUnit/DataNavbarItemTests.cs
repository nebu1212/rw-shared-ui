using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Tests.xUnit;

public sealed class DataNavbarItemTests
{
    [Fact]
    public void Constructor_MinimalValues_SetsDefaults()
    {
        var item = new NavbarItem("id", "Home", "/");

        Assert.Equal("id", item.Id);
        Assert.Equal("Home", item.Text);
        Assert.Equal("/", item.Href);
        Assert.Null(item.Icon);
        Assert.Null(item.ParentId);
        Assert.Equal(0, item.Order);
    }

    [Fact]
    public void Constructor_AllValues_AreAssigned()
    {
        var item = new NavbarItem(
            id: "profile",
            text: "Profile",
            href: "/profile",
            icon: "user",
            parentId: "root",
            order: 3);

        Assert.Equal("profile", item.Id);
        Assert.Equal("Profile", item.Text);
        Assert.Equal("/profile", item.Href);
        Assert.Equal("user", item.Icon);
        Assert.Equal("root", item.ParentId);
        Assert.Equal(3, item.Order);
    }
    
    [Fact]
    public void Constructor_Allows_NullOrEmpty_OptionalValues()
    {
        var item = new NavbarItem("id", "Text", "/path", null, null);

        Assert.Null(item.Icon);
        Assert.Null(item.ParentId);
    }

    [Fact]
    public void Constructor_Allows_ZeroAndNegativeOrder()
    {
        var zero = new NavbarItem("id1", "A", "/", order: 0);
        var negative = new NavbarItem("id2", "B", "/", order: -5);

        Assert.Equal(0, zero.Order);
        Assert.Equal(-5, negative.Order);
    }
    
    [Fact]
    public void NavbarItem_Properties_AreReadOnly()
    {
        var item = new NavbarItem("id", "Text", "/");

        var props = typeof(NavbarItem).GetProperties();

        Assert.All(props, p =>
            Assert.False(p.CanWrite, $"Property {p.Name} should be read-only"));
    }
    
    [Fact]
    public void Ctor_Allows_Nulls_For_OptionalFields_And_DefaultOrder()
    {
        var item = new NavbarItem("id", "text");

        Assert.Equal("id", item.Id);
        Assert.Equal("text", item.Text);
        Assert.Null(item.Href);
        Assert.Null(item.Icon);
        Assert.Null(item.ParentId);
        Assert.Equal(0, item.Order);
    }

    [Fact]
    public void Ctor_NullId_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new NavbarItem(null!, "text"));
    }

    [Fact]
    public void Ctor_NullText_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new NavbarItem("id", null!));
    }
}