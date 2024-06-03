using System.Text.RegularExpressions;
using Visicom.DataApi.Geocoder;
using Visicom.DataApi.Geocoder.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Service;

public partial class VisicomMapPointProvider
    (HttpClient httpClient, IRequestOptions options, ILogger<VisicomMapPointProvider> logger) 
    : BasicGeocoder(httpClient, options), IMapPointProvider
{
    private const string RegexDistrictPattern = @"[А-Яа-яіІ-]+ (обл\.|область)";
    private const string RegexAddressPattern = @"((м\.|смт\.|с\.|смт|пгт\.)\s*[А-Яа-яіІ-]+)((.*\d[а-я]{1})|(.*\d))";
    private readonly ILogger<VisicomMapPointProvider> logger = logger;

    [GeneratedRegex(RegexAddressPattern)]
    private static partial Regex AddressRegex();
    [GeneratedRegex(RegexDistrictPattern)]
    private static partial Regex DistrictRegex();
    
    public async Task<MapPoint> GetPoint(string address)
    {
        logger.LogTrace("Getting map point for {address}", address);
        string fullAddress = ExtractAddress(address);

        var coordinates = await GetCoordinatesAsync(fullAddress);
        logger.LogTrace("Got coordinates {coords} for {address}", coordinates, fullAddress);
        return new MapPoint(coordinates.Latitude, coordinates.Longitude);
    }

    private static string ExtractAddress(string address)
    {
        var addressMatch = AddressRegex().Match(address);
        var districtMatch = DistrictRegex().Match(address);
        var fullAddress = string.Empty;

        if (!string.IsNullOrWhiteSpace(districtMatch.ToString()))
        {
            fullAddress += districtMatch + " ";
        }
        fullAddress += addressMatch;

        return fullAddress;
    }
}
