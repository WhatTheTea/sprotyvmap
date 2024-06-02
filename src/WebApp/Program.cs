using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WhatTheTea.SprotyvMap.WebApp;
using WhatTheTea.SprotyvMap.WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();
builder.Services.AddCors(options =>
{
    var list = builder.Configuration.GetSection("cors:urls").Get<List<string>>();
    if (list != null)
    {
        options.AddDefaultPolicy(
                     policy =>
                     {
                         policy.WithOrigins(list?.ToArray());
                     });
    }

});
builder.Services.AddScoped<IEquipmentCentreProvider, ApiEquipmentCentreProvider>();

var app = builder.Build();
await app.RunAsync();