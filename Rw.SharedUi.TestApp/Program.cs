using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rw.SharedUi.TestApp;
using MudBlazor.Services;
using Rw.SharedUi.Contracts;
using Rw.SharedUi.TestApp.Layout;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddScoped<ILayoutContext, AppLayoutContext>();


await builder.Build().RunAsync();
