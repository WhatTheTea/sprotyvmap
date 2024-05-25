using Visicom.DataApi.Geocoder;
using Visicom.DataApi.Geocoder.Abstractions;
using Visicom.DataApi.Geocoder.Enums;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Service;

public class VisicomMapPointProvider : BasicGeocoder, IMapPointProvider
{
    // TODO: filter address
    public async Task<MapPoint> GetPoint(string address) => await GetCoordinatesAsync(address);

    public VisicomMapPointProvider(HttpClient httpClient, IRequestOptions options) : base(httpClient, options)
    {
    }
}