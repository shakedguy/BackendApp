using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazored.LocalStorage;
using MudBlazor.Utilities;

namespace BackendApp.Client.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        private string m_themeName = "light";

        private readonly MudTheme m_darkTheme = new MudTheme
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

        private readonly MudTheme m_lightTheme = new MudTheme
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

        private MudTheme m_currentTheme;
        public MainLayout() => this.m_currentTheme = m_lightTheme;

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorage.ContainKeyAsync("theme"))
                m_themeName = await LocalStorage.GetItemAsStringAsync("theme");
            else
                m_themeName = "light";

            m_currentTheme = m_themeName == "light" ? m_lightTheme : m_darkTheme;
        }

        private async Task ChangeThemeAsync()
        {
            if (m_themeName == "light")
            {
                m_currentTheme = m_darkTheme;
                m_themeName = "dark";
            }
            else
            {
                m_currentTheme = m_lightTheme;
                m_themeName = "light";
            }

            await LocalStorage.SetItemAsStringAsync("theme", m_themeName);
        }
    }
}
