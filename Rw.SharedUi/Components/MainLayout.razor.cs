using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.Themes;

namespace Rw.SharedUi.Components;

/// <summary>
/// Provides the main shell layout, wiring UI events to the shared layout context.
/// </summary>
public partial class MainLayout : LayoutComponentBase, IDisposable
{
    /// <summary>
    /// Gets or sets the layout context that supplies UI state.
    /// </summary>
    [Inject]
    public required ILayoutContext LayoutContext { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation manager used to react to route changes.
    /// </summary>
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    /// <summary>
    /// Reference to the MudBlazor theme provider in the view.
    /// </summary>
    private MudThemeProvider? _mudThemeProvider;
    
    /// <summary>
    /// Shared theme instance used throughout the layout.
    /// </summary>
    private readonly MudTheme _currentTheme = ThemeProvider.Theme;
    
    /// <summary>
    /// Tracks whether the layout is currently in dark mode.
    /// </summary>
    private bool _isDarkMode;
    
    /// <summary>
    /// Lookup of navigation children keyed by parent identifier.
    /// </summary>
    private Dictionary<string, List<NavbarItem>> _navIndex = new(StringComparer.Ordinal);
    
    /// <summary>
    /// Map of normalized href values to their corresponding navigation identifiers.
    /// </summary>
    private Dictionary<string, string> _hrefToId = new(StringComparer.OrdinalIgnoreCase);
    
    /// <summary>
    /// Tracks the currently active navigation item identifier.
    /// </summary>
    private string? _activeId;

    /// <summary>
    /// Initializes the layout by building navigation indexes and applying the theme.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        this.LayoutContext.Changed += OnLayoutContextChanged;
        this.LayoutContext.ThemeModeChanged += OnThemeModeChanged;

        await this.LayoutContext.InitializeAsync();
        
        BuildNavIndex();
        
        this._activeId = ResolveActiveIdByBacktracking(NavigationManager.Uri);
        this.NavigationManager.LocationChanged += OnLocationChanged;
        
        await ApplyThemeAsync(LayoutContext.ThemeMode);
    }
    
    /// <summary>
    /// Handles navigation changes and updates the active navigation item.
    /// </summary>
    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        this._activeId = ResolveActiveIdByBacktracking(e.Location);
        _ = InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Responds to layout context updates by triggering a re-render on the UI thread.
    /// </summary>
    private void OnLayoutContextChanged()
    {
        // Auf UI-Thread neu rendern
        BuildNavIndex();
        _ = InvokeAsync(StateHasChanged);
    }
    
    /// <summary>
    /// Reacts to theme changes by applying the new mode and refreshing the UI.
    /// </summary>
    private void OnThemeModeChanged(ThemeMode mode)
    {
        _ = InvokeAsync(async () =>
        {
            await ApplyThemeAsync(mode);
            StateHasChanged();
        });
    }
    
    /// <summary>
    /// Toggles the sidebar open or closed.
    /// </summary>
    protected void OnToggleSidebarClicked()
    {
        LayoutContext.ToggleSidebar();
    }
    
    /// <summary>
    /// Gets or sets whether the sidebar is open via the layout context.
    /// </summary>
    private bool SidebarOpen
    {
        get => LayoutContext.IsSidebarOpen;
        set => LayoutContext.SetSidebarOpen(value);
    }

    /// <summary>
    /// Toggles the current theme mode using the layout context.
    /// </summary>
    protected async Task OnThemeToggleClicked()
    {
        await LayoutContext.ToggleThemeAsync();
    }
    
    /// <summary>
    /// Sets the theme mode explicitly.
    /// </summary>
    /// <param name="mode">The mode to apply.</param>
    private void SetTheme(ThemeMode mode)
    {
        LayoutContext.SetThemeMode(mode);
    }

    /// <summary>
    /// Handles clicks on profile menu items.
    /// </summary>
    /// <param name="item">The clicked menu item.</param>
    protected async Task OnProfileMenuItemClick(ProfileMenuItem item)
    {
        await LayoutContext.OnProfileMenuItemClickedAsync(item);
    }

    /// <summary>
    /// Unregisters event handlers when the component is disposed.
    /// </summary>
    public void Dispose()
    {
        LayoutContext.Changed -= OnLayoutContextChanged;
        LayoutContext.ThemeModeChanged -= OnThemeModeChanged;
        NavigationManager.LocationChanged -= OnLocationChanged;

    }
    
    /// <summary>
    /// Returns the icon name that corresponds to the current theme mode.
    /// </summary>
    /// <returns>The Material icon identifier.</returns>
    protected string GetThemeIcon()
    {
        return LayoutContext.ThemeMode switch
        {
            ThemeMode.Dark   => Icons.Material.Filled.DarkMode,
            ThemeMode.Light  => Icons.Material.Filled.LightMode,
            _                => Icons.Material.Filled.Computer
        };
    }
    
    /// <summary>
    /// Returns the label describing the current theme selection.
    /// </summary>
    /// <returns>A human-readable theme description.</returns>
    protected string GetThemeLabel()
    {
        return LayoutContext.ThemeMode switch
        {
            ThemeMode.Dark   => "Dark Theme",
            ThemeMode.Light  => "Light Theme",
            _                => "System Theme"
        };
    }

    /// <summary>
    /// Toggles the theme mode via the profile menu entry.
    /// </summary>
    protected async Task OnThemeToggleMenuItemClick()
    {
        await LayoutContext.ToggleThemeAsync();
    }

    /// <summary>
    /// Applies the given theme mode by updating local state or reading the system setting.
    /// </summary>
    /// <param name="mode">The theme mode to apply.</param>
    /// <returns>A task that completes when the mode has been processed.</returns>
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
    
    /// <summary>
    /// Builds lookups for rendering navigation structures efficiently.
    /// </summary>
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
    
    /// <summary>
    /// Normalizes paths or URIs for consistent comparison.
    /// </summary>
    /// <param name="uriOrPath">The URI or path to normalize.</param>
    /// <returns>The normalized path or null if invalid.</returns>
    private string? NormalizePath(string? uriOrPath)
    {
        if (string.IsNullOrWhiteSpace(uriOrPath))
        {
            return null;
        }
        
        try
        {
            var s = uriOrPath.Split('?', '#')[0].Trim();
            
            if (string.IsNullOrEmpty(s))
                return "/";


            if (Uri.TryCreate(s, UriKind.Absolute, out var abs))
            {
                // wenn unter BaseUri => BaseRelative (entfernt /installer/ etc.)
                try
                {
                    // ToBaseRelativePath knallt, wenn abs nicht unter BaseUri ist
                    var rel = NavigationManager.ToBaseRelativePath(abs.ToString());
                    s = "/" + rel;
                }
                catch
                {
                    // externe/andere Base -> nur Path nehmen
                    s = abs.AbsolutePath;
                }
            }

            if (!s.StartsWith('/'))
            {
                s = "/" + s;
            }

            return s.Length > 1 ? s.TrimEnd('/') : "/";
        }
        catch
        {
            return null;
        }
    }
    
    /// <summary>
    /// Attempts to resolve the active navigation identifier by backtracking path segments.
    /// </summary>
    /// <param name="uriOrPath">The URI or path to evaluate.</param>
    /// <returns>The matching navigation item identifier, if any.</returns>
    private string? ResolveActiveIdByBacktracking(string uriOrPath)
    {
        var path = NormalizePath(uriOrPath);
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        while (true)
        {
            if (_hrefToId.TryGetValue(path, out var id))
            {
                return id;
            }

            if (path == "/")
            {
                return null;
            }
                
            var cut = path.LastIndexOf('/');
            path = cut <= 0 ? "/" : path[..cut];
        }
    }
    
    /// <summary>
    /// Determines whether a navigation subtree contains the active item.
    /// </summary>
    /// <param name="parentId">The parent identifier to inspect.</param>
    /// <returns><c>true</c> if a descendant is active; otherwise, <c>false</c>.</returns>
    private bool SubtreeContainsActive(string parentId)
    {
        if (string.IsNullOrEmpty(_activeId))
        {
            return false;
        }

        if (!_navIndex.TryGetValue(parentId, out var children))
        {
            return false;
        }
        
        foreach (var child in children)
        {
            if (child.Id == _activeId)
            {
                return true;
            }

            if (_navIndex.ContainsKey(child.Id) && SubtreeContainsActive(child.Id))
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// Renders the navigation structure recursively for a given parent.
    /// </summary>
    /// <param name="parentId">The parent identifier to render children for.</param>
    /// <returns>A render fragment that produces the navigation markup.</returns>
    private RenderFragment RenderNav(string parentId) => builder =>
    {
        if (!_navIndex.TryGetValue(parentId, out var items))
        {
            return;
        }
        
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
            {
                continue;
            }

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