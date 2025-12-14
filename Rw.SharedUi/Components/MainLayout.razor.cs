using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.Themes;

namespace Rw.SharedUi.Components;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    [Inject]
    public required ILayoutContext LayoutContext { get; set; } 
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    
    private MudThemeProvider? _mudThemeProvider;

    private readonly MudTheme _currentTheme = ThemeProvider.Theme;
    private bool _isDarkMode;
    
    private Dictionary<string, List<NavbarItem>> _navIndex = new(StringComparer.Ordinal);
    private Dictionary<string, string> _hrefToId = new(StringComparer.OrdinalIgnoreCase);
    private string? _activeId;


    protected override async Task OnInitializedAsync()
    {
        LayoutContext.Changed += OnLayoutContextChanged;
        LayoutContext.ThemeModeChanged += OnThemeModeChanged;

        BuildNavIndex();
        
        _activeId = ResolveActiveIdByBacktracking(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
        
        await ApplyThemeAsync(LayoutContext.ThemeMode);
    }
    
    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _activeId = ResolveActiveIdByBacktracking(e.Location);
        _ = InvokeAsync(StateHasChanged);
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
        NavigationManager.LocationChanged -= OnLocationChanged;

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
    
    private void BuildNavIndex()
    {
        var items = LayoutContext.NavbarItems;

        _navIndex = items
            .GroupBy(x => string.IsNullOrWhiteSpace(x.ParentId) ? string.Empty : x.ParentId!)
            .ToDictionary(g => g.Key, g => g.OrderBy(x => x.Order).ToList(), StringComparer.Ordinal);

        _hrefToId = items
            .Where(i => !string.IsNullOrWhiteSpace(i.Href))
            .Select(i => new { i.Id, Path = NormalizePath(i.Href) })
            .Where(x => !string.IsNullOrWhiteSpace(x.Path))
            .ToDictionary(x => x.Path!, x => x.Id, StringComparer.OrdinalIgnoreCase);
    }
    
    private string? NormalizePath(string? uriOrPath)
    {
        if (string.IsNullOrWhiteSpace(uriOrPath))
            return null;

        try
        {
            var s = uriOrPath.Split('?', '#')[0].Trim();

            // Wenn absolute URL: nur Pfad nehmen
            if (Uri.TryCreate(s, UriKind.Absolute, out var abs))
                s = abs.AbsolutePath;

            if (string.IsNullOrWhiteSpace(s))
                return "/";

            if (!s.StartsWith('/'))
                s = "/" + s;

            return s.Length > 1 ? s.TrimEnd('/') : "/";
        }
        catch
        {
            return null;
        }
    }
    
    private string? ResolveActiveIdByBacktracking(string uriOrPath)
    {
        var path = NormalizePath(uriOrPath);
        if (string.IsNullOrWhiteSpace(path))
            return null;

        while (true)
        {
            if (_hrefToId.TryGetValue(path, out var id))
                return id;

            if (path == "/")
                return null;

            var cut = path.LastIndexOf('/');
            path = cut <= 0 ? "/" : path[..cut];
        }
    }
    
    private bool SubtreeContainsActive(string parentId)
    {
        if (string.IsNullOrEmpty(_activeId))
            return false;

        if (!_navIndex.TryGetValue(parentId, out var children))
            return false;

        foreach (var child in children)
        {
            if (child.Id == _activeId)
                return true;

            if (_navIndex.ContainsKey(child.Id) && SubtreeContainsActive(child.Id))
                return true;
        }

        return false;
    }
    
    private RenderFragment RenderNav(string parentId) => builder =>
    {
        if (!_navIndex.TryGetValue(parentId, out var items))
            return;
        
        var seq = 0;

        foreach (var item in items)
        {
            var hasChildren = _navIndex.ContainsKey(item.Id);

            if (hasChildren)
            {
                builder.OpenComponent<MudNavGroup>(seq++);
                builder.SetKey(item.Id);
                builder.AddAttribute(seq++, "Title", item.Text);
                builder.AddAttribute(seq++, "Icon", item.Icon);
                builder.AddAttribute(seq++, "Expanded", SubtreeContainsActive(item.Id)); // <- AUFKLAPPEN
                builder.AddAttribute(seq++, "ChildContent", RenderNav(item.Id));
                builder.CloseComponent();
                continue;
            }

            if (string.IsNullOrWhiteSpace(item.Href))
                continue;

            builder.OpenComponent<MudNavLink>(seq++);
            builder.SetKey(item.Id);

            builder.AddAttribute(seq++, "Href", item.Href);
            builder.AddAttribute(seq++, "Icon", item.Icon);
            builder.AddAttribute(seq++, "Match", NavLinkMatch.All);

            builder.AddAttribute(seq++, "Class", _activeId == item.Id ? "rw-nav-link-active" : null);

            builder.AddAttribute(seq++, "ChildContent",
                (RenderFragment)(b => b.AddContent(0, item.Text)));

            builder.CloseComponent();
        }
    };
}