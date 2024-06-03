using Visicom.DataApi.Geocoder.Enums;
using Visicom.DataApi.Geocoder;
using WhatTheTea.SprotyvMap.Service;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.SprotyvInUa;

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Extensions;

public static class EquipmentCentresExtensions
{
    public static IServiceCollection AddEquipmentCentreDataProviders(this IServiceCollection services, string visicomApiKey) 
    {
        var requestOptions = new RequestOptions(Languages.Ukrainian, visicomApiKey);

        services.AddScoped<IMapPointProvider, VisicomMapPointProvider>(services =>
                new VisicomMapPointProvider(
                    services.GetRequiredService<HttpClient>(),
                    requestOptions,
                    services.GetRequiredService<ILogger<VisicomMapPointProvider>>())
                );

        services.AddScoped<IDataProvider, WebScraper>(services =>
            WebScraper.Create(services.GetRequiredService<HttpClient>())
            .GetAwaiter()
            .GetResult());

        return services;
    }
}
