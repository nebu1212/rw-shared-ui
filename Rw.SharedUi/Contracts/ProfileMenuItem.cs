namespace Rw.SharedUi.Contracts;

/// <summary>
/// Represents an action that can be triggered from a user's profile menu.
/// </summary>
public sealed class ProfileMenuItem
{
    /// <summary>
    /// Gets the unique identifier of the menu item.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the text displayed for the menu item.
    /// </summary>
    public string Text { get; }
    
    /// <summary>
    /// Gets the optional icon associated with the menu item.
    /// </summary>
    public string? Icon { get; }
    
    /// <summary>
    /// Gets the ordering value used to sort menu items.
    /// </summary>
    public int Order { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileMenuItem"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the menu item.</param>
    /// <param name="text">The label displayed to the user.</param>
    /// <param name="icon">The optional icon name.</param>
    /// <param name="order">The sort order for the item.</param>
    public ProfileMenuItem(string id, string text, string? icon = null, int order = 0)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Icon = icon;
        Order = order;
    }
}