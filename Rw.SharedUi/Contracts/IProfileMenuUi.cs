namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines the UI elements for a profile menu component.
/// </summary>
public interface IProfileMenuUi
{
    /// <summary>
    /// Gets the list of profile menu items.
    /// </summary>
    IReadOnlyList<ProfileMenuItem> ProfileMenuItems { get; }

    /// <summary>
    /// Handles the click event for a profile menu item.
    /// </summary>
    Task OnProfileMenuItemClickedAsync(ProfileMenuItem item);

}