using WhatTheTea.SprotyvMap.WebService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddLogging();
builder.Services.AddHttpClient();

var visicomApiKey = builder.Configuration.GetValue<string>("VISICOM_DAPI_KEY") ??
                    throw new InvalidOperationException("Can't load Visicom Data API key");

builder.Services.AddEquipmentCentreDataProviders(visicomApiKey);
builder.Services.AddEquipmentCentreCachedService();

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
