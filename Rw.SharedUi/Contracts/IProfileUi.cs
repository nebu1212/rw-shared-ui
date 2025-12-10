namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines the UI elements for a user profile component.
/// </summary>
public interface IProfileUi
{
    /// <summary>
    /// Gets the user's display name.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Gets the user's profile image URL.
    /// </summary>
    string? ProfileImageUrl { get; }

}