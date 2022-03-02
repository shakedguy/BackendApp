using System.Runtime.CompilerServices;
using BackendApp.Shared;
using BackendApp.Shared.Helpers;
using BackendApp.Shared.Models;
using DataLibrary;
using EmbeddedBlazorContent;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    //EnvironmentName = Environments.Production,
});

//new WebApplicationOptions
//{
//    EnvironmentName = Environments.Development,
//}

HostConfig.CrtPath = builder.Configuration["Certificate:Path"];
HostConfig.CrtPassword = builder.Configuration["Certificate:Password"];
HostConfig.Port = int.Parse(builder.Configuration["Port"]);

builder.WebHost.ConfigureKestrel(opt =>
{
    opt.Listen(HostConfig.Domain, HostConfig.Port + 1);
    opt.Listen(HostConfig.Domain, HostConfig.Port, listenOpt =>
    {
        listenOpt.UseHttps(HostConfig.CrtPath, HostConfig.CrtPassword);
    });
});


builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddSingleton<IModel, MessageModel>();
builder.Services.AddSingleton<IModel, DivisionModel>();

DbContext.ConnectionString = builder.Configuration["MySqlConnectionString"];


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();


app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
