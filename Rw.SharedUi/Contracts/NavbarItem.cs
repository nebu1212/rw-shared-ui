namespace Rw.SharedUi.Contracts;

/// <summary>
/// Represents an item that can be displayed inside a navigation bar or sidebar.
/// </summary>
public sealed class NavbarItem
{
    /// <summary>
    /// Gets the unique identifier for the navigation item.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the label shown for the navigation entry.
    /// </summary>
    public string Text { get; }
    
    /// <summary>
    /// Gets the optional target hyperlink for the navigation entry.
    /// </summary>
    public string? Href { get; }
    
    /// <summary>
    /// Gets the name of the icon to render alongside the text.
    /// </summary>
    public string? Icon { get; } 
    
    /// <summary>
    /// Gets the optional identifier of a parent item when representing nested navigation.
    /// </summary>
    public string? ParentId { get; } 
    
    /// <summary>
    /// Gets the ordering value used to sort navigation items.
    /// </summary>
    public int Order { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NavbarItem"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the navigation item.</param>
    /// <param name="text">The label displayed to the user.</param>
    /// <param name="href">The optional hyperlink for navigation.</param>
    /// <param name="icon">The optional icon name.</param>
    /// <param name="parentId">The optional identifier of a parent navigation item.</param>
    /// <param name="order">The sort order for the item.</param>
    public NavbarItem(string id, string text, string? href = null, string? icon = null, string? parentId = null, int order = 0)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Href = href;
        Icon = icon;
        ParentId = parentId;
        Order = order;
    }
}