using Blazored.LocalStorage;
using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.TestApp.Storage;

/// <summary>
/// Blazored.LocalStorage implementation for persisting ThemeMode.
/// </summary>
public sealed class BlazoredThemeModeStore : IThemeModeStore
{
    private const string Prefix = "rw.testapp.ui.";
    private const string Key = Prefix + "themeMode";
    private readonly ILocalStorageService _localStorage;

    public BlazoredThemeModeStore(ILocalStorageService localStorage)
    {
        this._localStorage = localStorage;
    }

    public async Task<ThemeMode?> LoadAsync(CancellationToken ct = default)
    {
        if (!await this._localStorage.ContainKeyAsync(Key, ct))
        {
            return null;
        }
        
        var raw = await this._localStorage.GetItemAsync<string>(Key, ct);

        if (Enum.TryParse<ThemeMode>(raw, ignoreCase: true, out var mode))
        {
            return mode;
        }

        return null;
    }

    public async Task SaveAsync(ThemeMode mode, CancellationToken ct = default)
    {
        await this._localStorage.SetItemAsync(Key, mode.ToString(), ct);
    }
}