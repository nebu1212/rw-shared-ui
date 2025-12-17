namespace Rw.SharedUi.Contracts;

/// <summary>
/// Defines methods for persisting and retrieving the user's sidebar open preference.
/// </summary>
public interface ISidebarStateStore
{
    /// <summary>
    /// Loads the saved sidebar open preference.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool?> LoadAsync(CancellationToken ct = default);
    
    /// <summary>
    /// Saves the specified sidebar open preference.
    /// </summary>
    /// <param name="isOpen"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task SaveAsync(bool isOpen, CancellationToken ct = default);
}