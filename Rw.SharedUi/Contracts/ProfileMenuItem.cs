namespace Rw.SharedUi.Contracts;

public sealed class ProfileMenuItem
{
    public string Id { get; }
    public string Text { get; }
    public string? Icon { get; } 
    public int Order { get; }

    public ProfileMenuItem(string id, string text, string? icon = null, int order = 0)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Icon = icon;
        Order = order;
    }
}