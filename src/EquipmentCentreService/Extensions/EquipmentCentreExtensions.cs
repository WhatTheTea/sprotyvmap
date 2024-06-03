﻿using Visicom.DataApi.Geocoder.Enums;
using Visicom.DataApi.Geocoder;
using WhatTheTea.SprotyvMap.Service;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.SprotyvInUa;
using WhatTheTea.SportyvMap.EquipmentCentreService.Services;
using WhatTheTea.SportyvMap.EquipmentCentreService.Workers;
using WhatTheTea.SprotyvMap.Shared.Primitives;
using Microsoft.Extensions.Caching.Memory;

namespace WhatTheTea.SportyvMap.EquipmentCentreService.Extensions;

public static class EquipmentCentresExtensions
{
    public static IServiceCollection AddEquipmentCentreDataProviders(this IServiceCollection services, string visicomApiKey) 
    {
        var requestOptions = new RequestOptions(Languages.Ukrainian, visicomApiKey);

        services.AddSingleton<IMapPointProvider, VisicomMapPointProvider>(services =>
                new VisicomMapPointProvider(
                    services.GetRequiredService<HttpClient>(),
                    requestOptions,
                    services.GetRequiredService<ILogger<VisicomMapPointProvider>>())
                );

        services.AddSingleton<IDataProvider, WebScraper>(services =>
            WebScraper.Create(services.GetRequiredService<HttpClient>())
            .GetAwaiter()
            .GetResult());

        return services;
    }

    public static IServiceCollection AddEquipmentCentreCachedService(this IServiceCollection services) 
    {
        services.AddMemoryCache();
        services.AddSingleton(typeof(CacheSignal<>));

        services.AddSingleton<IEquipmentCentreService, VisicomEquipmentCentreService>();
        services.AddHostedService(serviceProvider =>
            new DistrictsCacheWorker(
                EquipmentCentreServiceFactory(serviceProvider),
                serviceProvider.GetRequiredService<CacheSignal<District[]>>(),
                serviceProvider.GetRequiredService<IMemoryCache>(),
                serviceProvider.GetRequiredService<ILogger<DistrictsCacheWorker>>())
            );
        services.AddSingleton<IEquipmentCentreService, CachedEquipmentCentreService>();

        return services;
    }

    private static IEquipmentCentreService EquipmentCentreServiceFactory(IServiceProvider serviceProvider)
        => new VisicomEquipmentCentreService(
            serviceProvider.GetRequiredService<IDataProvider>(),
            serviceProvider.GetRequiredService<IMapPointProvider>());

}
