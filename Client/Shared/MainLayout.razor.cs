using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazored.LocalStorage;
using MudBlazor.Utilities;

namespace BackendApp.Client.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        bool _drawerOpen = true;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected async override Task OnInitializedAsync()
        {
            if (await LocalStorage.ContainKeyAsync("theme"))
                _themeName = await LocalStorage.GetItemAsStringAsync("theme");
            else
                _themeName = "light";

            _currentTheme = _themeName == "light" ? _lightTheme : _darkTheme;
        }

        MudTheme _currentTheme = null;

        private string _themeName = "light";

        MudTheme _darkTheme = new MudTheme
        {
            Palette = new Palette
            {
                AppbarBackground = "#0097FF",
                AppbarText = "#FFFFFF",
                Primary = "#007CD1",
                TextPrimary = "#FFFFFF",
                Background = "#2C3E50",
                TextSecondary = "#E2EEF6",
                DrawerBackground = "#093958",
                DrawerText = "#FFFFFF",
                Surface = "#093958",
                ActionDefault = "#0C1217",
                ActionDisabled = "#2F678C",
                TextDisabled = "#B0B0B0",
                TableLines = "#343434",
                TableStriped = "#343434",
                TableHover = "#007CD1",
                PrimaryLighten = "#0097FF",
                SecondaryLighten = "#0097FF",
                OverlayLight = "#0097FF"
            }
        };

        MudTheme _lightTheme = new MudTheme
        {
            Palette = new Palette
            {
                AppbarBackground = "#E4FAFF",
                AppbarText = "#FFFFFF",
                Primary = "#007CD1",
                TextPrimary = "#0C1217",
                Background = "#F5F5F5",
                TextSecondary = "#0C1217",
                DrawerText = "#0C1217",
                Surface = "#E4FAFF",
                ActionDefault = "#0C1217",
                ActionDisabled = "#2F678C",
                TextDisabled = "#676767",
                TableLines = "#FFFFFF",
                TableStriped = "#F5F5F5",
                TableHover = "#007CD1",
            }
        };

        private async Task ChangeThemeAsync()
        {
            if (_themeName == "light")
            {
                _currentTheme = _darkTheme;
                _themeName = "dark";
            }
            else
            {
                _currentTheme = _lightTheme;
                _themeName = "light";
            }

            await LocalStorage.SetItemAsStringAsync("theme", _themeName);
        }
    }
}
