using System.Runtime.InteropServices;
using System.Text.Json;
using BackendApp.Shared.Models;
using BackendApp.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor.Extensions;

namespace BackendApp.Client.States
{
    public class GenericPageState
    {
        private readonly FunctionsHelperClass m_helper;

        private readonly NavigationManager m_navigationManager;
        public GenericPageState(FunctionsHelperClass i_Helper, NavigationManager i_NavigationManager)
        {
            m_helper = i_Helper;
            m_navigationManager = i_NavigationManager;
        }

        public int CurrentPage { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string DialogTitle { get; private set; } = string.Empty;

        private IEnumerable<IModel>? m_items = new List<IModel>();
        public IEnumerable<IModel>? Data
        {
            get => this.m_items!;
            set
            {
                this.m_items = value;
                this.DialogTitle = m_items!.ElementAt(0).ToString()!;
                this.Title = DialogTitle + "s";
            }

        }
        public IModel? SelectedItem { get; set; } = null;
        public IModel this[int index] { get => this.Data!.ElementAt(index) as IModel; }

    }
}
