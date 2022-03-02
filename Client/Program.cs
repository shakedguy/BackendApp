using BackendApp.Client;
using BackendApp.Client.States;
using BackendApp.Shared.Helpers;
using BackendApp.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<GenericPageState>();
builder.Services.AddScoped<DialogState>();
builder.Services.AddTransient<FunctionsHelperClass>();
builder.Services.AddSingleton<IModel, MessageModel>();
builder.Services.AddSingleton<IModel, DivisionModel>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomStart;
    config.SnackbarConfiguration.VisibleStateDuration = 1250;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;

});
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
