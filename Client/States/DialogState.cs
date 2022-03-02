using BackendApp.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BackendApp.Client.States
{
    public delegate void OnCloseEventHandler();
    public class DialogState
    {
        public OnCloseEventHandler? OnClose { get; set; } = null;
        public bool Cancelled { get; set; } = false;

        public string Data { get; set; } = string.Empty;
        public void Close() => OnClose!.Invoke();

    }
}
