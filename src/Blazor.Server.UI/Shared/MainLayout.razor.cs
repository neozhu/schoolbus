using Blazor.Server.UI.Components.Shared;
using Blazor.Server.UI.Services.Layout;
using Blazor.Server.UI.Services.UserPreferences;
using Microsoft.AspNetCore.Components.Web;


namespace Blazor.Server.UI.Shared;

public partial class MainLayout : LayoutComponentBase, IDisposable
{
    private bool _commandPaletteOpen;

    private bool _navigationMenuDrawerOpen = true;
    private UserPreferences _userPreferences = new();
    [Inject]
    private LayoutService LayoutService { get; set; } = null!;
    private MudThemeProvider _mudThemeProvider=null!;
    private bool _themingDrawerOpen;
    private bool _defaultDarkMode;


    private ErrorBoundary? _errorBoundary { set; get; } = null!;

    protected override void OnParametersSet()
    {
        ResetBoundary();
    }

    private void ResetBoundary()
    {
        // On each page navigation, reset any error state
        _errorBoundary?.Recover();
    }
    public void Dispose()
    {
        LayoutService.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured;
        GC.SuppressFinalize(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ApplyUserPreferences();

            StateHasChanged();
        }
    }
    private async Task ApplyUserPreferences()
    {
        _userPreferences = await LayoutService.ApplyUserPreferences(false);
    }
    protected override void OnInitialized()
    {
        LayoutService.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured;
       
    }

    private void LayoutServiceOnMajorUpdateOccured(object? sender, EventArgs e) => StateHasChanged();



    protected void NavigationMenuDrawerOpenChangedHandler(bool state)
    {
        _navigationMenuDrawerOpen = state;
    }
    protected void ThemingDrawerOpenChangedHandler(bool state)
    {
        _themingDrawerOpen = state;
    }
    protected void ToggleNavigationMenuDrawer()
    {
        _navigationMenuDrawerOpen = !_navigationMenuDrawerOpen;
    }
    private async Task OpenCommandPalette()
    {
        if (!_commandPaletteOpen)
        {
            var options = new DialogOptions
            {
                NoHeader = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            var commandPalette = DialogService.Show<CommandPalette>("", options);
            _commandPaletteOpen = true;

            await commandPalette.Result;
            _commandPaletteOpen = false;
        }
    }


}