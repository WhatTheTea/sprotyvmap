using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WhatTheTea.SprotyvMap.WebApp;
using WhatTheTea.SprotyvMap.WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();


string equipmentCentreApiUrl = builder.Configuration.GetValue<string>("EquipmentCentres_API_URL") ??
        throw new InvalidOperationException("Can't load EquipmentCentres API URL");

builder.Services.AddScoped<IEquipmentCentreProvider, ApiEquipmentCentreProvider>(
    options =>
    new ApiEquipmentCentreProvider(
        options.GetRequiredService<HttpClient>(),
        equipmentCentreApiUrl
        ));

var app = builder.Build();

await app.RunAsync();