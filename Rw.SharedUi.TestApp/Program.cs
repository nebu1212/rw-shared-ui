using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rw.SharedUi.TestApp;
using MudBlazor.Services;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.TestApp.Layout;
using Rw.SharedUi.TestApp.Storage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage(); 

builder.Services.AddScoped<ILayoutContext, AppLayoutContext>();

builder.Services.AddScoped<IThemeModeStore, BlazoredThemeModeStore>();
builder.Services.AddScoped<ISidebarStateStore, BlazoredSidebarStateStore>();



await builder.Build().RunAsync();
