using Microsoft.AspNetCore.Components;
using MudBlazor;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.Themes;

namespace Rw.SharedUi.Components;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject]
    public ILayoutContext LayoutContext { get; set; } = null!;
    
    private MudThemeProvider? _mudThemeProvider;

    private readonly MudTheme _currentTheme = ThemeProvider.Theme;
    private bool _isDarkMode;

    protected override async Task OnInitializedAsync()
    {
        LayoutContext.Changed += OnLayoutContextChanged;
        LayoutContext.ThemeModeChanged += OnThemeModeChanged;
        
        await ApplyThemeAsync(LayoutContext.ThemeMode);
    }

    private void OnLayoutContextChanged()
    {
        // Auf UI-Thread neu rendern
        _ = InvokeAsync(StateHasChanged);
    }
    
    private async void OnThemeModeChanged(ThemeMode mode)
    {
        await ApplyThemeAsync(mode);
        await InvokeAsync(StateHasChanged);
    }

    protected void OnToggleSidebarClicked()
    {
        LayoutContext.ToggleSidebar();
    }

    protected async Task OnThemeToggleClicked()
    {
        await LayoutContext.ToggleThemeAsync();
    }
    
    private void SetTheme(ThemeMode mode)
    {
        LayoutContext.SetThemeMode(mode);
    }

    protected async Task OnProfileMenuItemClick(ProfileMenuItem item)
    {
        await LayoutContext.OnProfileMenuItemClickedAsync(item);
    }

    public void Dispose()
    {
        LayoutContext.Changed -= OnLayoutContextChanged;
        LayoutContext.ThemeModeChanged -= OnThemeModeChanged;
    }
    
    protected string GetThemeIcon()
    {
        return LayoutContext.ThemeMode switch
        {
            ThemeMode.Dark   => Icons.Material.Filled.DarkMode,
            ThemeMode.Light  => Icons.Material.Filled.LightMode,
            _                => Icons.Material.Filled.Computer
        };
    }
    
    protected string GetThemeLabel()
    {
        return LayoutContext.ThemeMode switch
        {
            ThemeMode.Dark   => "Dark Theme",
            ThemeMode.Light  => "Light Theme",
            _                => "System Theme"
        };
    }

    protected async Task OnThemeToggleMenuItemClick()
    {
        await LayoutContext.ToggleThemeAsync();
        // LayoutContext triggert ThemeModeChanged + Changed,
        // StateHasChanged wird sowieso schon über Events aufgerufen.
    }

    
    
    private async Task ApplyThemeAsync(ThemeMode mode)
    {
        switch (mode)
        {
            case ThemeMode.Light:
                this._isDarkMode = false;
                break;
            case ThemeMode.Dark:
                this._isDarkMode = true;
                break;
            case ThemeMode.System:
            default:
                if (this._mudThemeProvider is not null)
                {
                    this._isDarkMode = await _mudThemeProvider.GetSystemDarkModeAsync();
                }
                else
                {
                    _isDarkMode = false;
                }
                break;
        }
    }
}