using BackendApp.Client;
using BackendApp.Client.States;
using BackendApp.Shared.Helpers;
using BackendApp.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<GenericPageState>();
builder.Services.AddTransient<FunctionsHelperClass>();
builder.Services.AddSingleton<IModel, MessageModel>();
builder.Services.AddSingleton<IModel, DivisionModel>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
