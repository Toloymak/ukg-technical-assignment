using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebUi;
using WebUi.Configurations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder
    .RegisterHttpClient()
    .RegisterAuth();

builder
    .Services
    .AddMudServices();


await builder
    .Build()
    .RunAsync();