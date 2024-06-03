using Microsoft.Extensions.Caching.Memory;

using WhatTheTea.SportyvMap.EquipmentCentreService.Extensions;
using WhatTheTea.SportyvMap.EquipmentCentreService.Services;
using WhatTheTea.SportyvMap.EquipmentCentreService.Workers;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddLogging();
builder.Services.AddHttpClient();

var visicomApiKey = builder.Configuration["VISICOM_DAPI_KEY"] ?? string.Empty;
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
