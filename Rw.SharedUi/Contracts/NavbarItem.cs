namespace Rw.SharedUi.Contracts;

public sealed class NavbarItem
{
    public string Id { get; }
    public string Text { get; }
    public string? Href { get; }
    public string? Icon { get; } 
    public string? ParentId { get; } 
    public int Order { get; }



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