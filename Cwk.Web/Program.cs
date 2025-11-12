using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cwk.Web;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var url = "http://localhost:5142";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });

builder.Services.AddMudServices();
await builder.Build().RunAsync();