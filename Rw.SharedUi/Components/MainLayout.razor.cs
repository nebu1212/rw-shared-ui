using Microsoft.AspNetCore.Components;
using MudBlazor;
using Rw.SharedUi.Contracts;

namespace Rw.SharedUi.Components;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject]
    public ILayoutContext LayoutContext { get; set; } = default!;

    protected override void OnInitialized()
    {
        // Auf Änderungen im LayoutContext reagieren
        LayoutContext.Changed += OnLayoutContextChanged;
    }

    private void OnLayoutContextChanged()
    {
        // Auf UI-Thread neu rendern
        _ = InvokeAsync(StateHasChanged);
    }

    protected void OnToggleSidebarClicked()
    {
        LayoutContext.ToggleSidebar();
    }

    protected async Task OnThemeToggleClicked()
    {
        await LayoutContext.ToggleThemeAsync();
    }

    protected async Task OnProfileMenuItemClick(ProfileMenuItem item)
    {
        await LayoutContext.OnProfileMenuItemClickedAsync(item);
    }

    public void Dispose()
    {
        LayoutContext.Changed -= OnLayoutContextChanged;
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
}