using Visicom.DataApi.Geocoder;
using Visicom.DataApi.Geocoder.Enums;
using WhatTheTea.SprotyvMap.Service;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.SprotyvInUa;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddControllers();

var visicomApiKey = builder.Configuration["VISICOM_DAPI_KEY"] ?? string.Empty;
var requestOptions = new RequestOptions(Languages.Ukrainian, visicomApiKey);
builder.Services
    .AddScoped<IMapPointProvider, VisicomMapPointProvider>(provider =>
        new VisicomMapPointProvider(provider.GetService<HttpClient>()!, requestOptions))
    .AddScoped<IDataProvider, WebScraper>(s => WebScraper.Create(s.GetService<HttpClient>()!).GetAwaiter().GetResult());

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

app.Run(url);
