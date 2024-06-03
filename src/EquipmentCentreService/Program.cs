using Visicom.DataApi.Geocoder;
using Visicom.DataApi.Geocoder.Enums;
using WhatTheTea.SprotyvMap.Service;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.SprotyvInUa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddHttpClient();

var visicomApiKey = builder.Configuration["VISICOM_DAPI_KEY"] ?? string.Empty;
var requestOptions = new RequestOptions(Languages.Ukrainian, visicomApiKey);

builder.Services
    .AddScoped<IMapPointProvider, VisicomMapPointProvider>(services =>
        new VisicomMapPointProvider(
            services.GetRequiredService<HttpClient>(),
            requestOptions,
            services.GetRequiredService<ILogger<VisicomMapPointProvider>>())
        );

builder.Services.AddScoped<IDataProvider, WebScraper>(services => 
    WebScraper.Create(services.GetRequiredService<HttpClient>())
    .GetAwaiter()
    .GetResult());

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
