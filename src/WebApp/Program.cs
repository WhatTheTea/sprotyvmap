using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WhatTheTea.SprotyvMap.WebApp;
using WhatTheTea.SprotyvMap.WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<IEquipmentCentreProvider, ApiEquipmentCentreProvider>();

await builder.Build().RunAsync();