using Blazored.LocalStorage;
using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.TestApp.Storage;

/// <summary>
/// Blazored.LocalStorage implementation for persisting sidebar open state.
/// </summary>
public sealed class BlazoredSidebarStateStore : ISidebarStateStore
{
    private const string Prefix = "rw.testapp.ui.";
    private const string Key = Prefix + "sidebarOpen";
    private readonly ILocalStorageService _localStorage;

    public BlazoredSidebarStateStore(ILocalStorageService localStorage)
    {
        this._localStorage = localStorage;
    }
    
    public async Task<bool?> LoadAsync(CancellationToken ct = default)
    {
        if (!await this._localStorage.ContainKeyAsync(Key, ct))
        {
            return null;
        }
        
        return await this._localStorage.GetItemAsync<bool>(Key, ct);
    }

    public async Task SaveAsync(bool isOpen, CancellationToken ct = default)
    {
        await this._localStorage.SetItemAsync(Key, isOpen, ct);
    }
        
}