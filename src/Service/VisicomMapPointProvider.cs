using System.Text.RegularExpressions;
using WhatTheTea.Visicom.Geocoder;
using WhatTheTea.Visicom.Geocoder.Abstractions;
using WhatTheTea.Visicom.Geocoder.Enums;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.Service;

public partial class VisicomMapPointProvider : BasicGeocoder, IMapPointProvider
{
    private const string RegexDistrictPattern = @"[А-Яа-яіІ-]+ (обл\.|область)";
    private const string RegexAddressPattern = @"((м\.|смт\.|с\.|смт|пгт\.)\s*[А-Яа-яіІ-]+)((.*\d[а-я]{1})|(.*\d))";
    
    [GeneratedRegex(RegexAddressPattern)]
    private static partial Regex AddressRegex();
    [GeneratedRegex(RegexDistrictPattern)]
    private static partial Regex DistrictRegex();
    
    public async Task<MapPoint> GetPoint(string address)
    {
        var addressMatch = AddressRegex().Match(address);
        var districtMatch = DistrictRegex().Match(address);
        // TODO: Refactor
        var fullAddress = string.Empty; 
        if (!string.IsNullOrWhiteSpace(districtMatch.ToString()))
        {
            fullAddress += districtMatch + " ";
        }
        fullAddress += addressMatch;
        
        return await GetCoordinatesAsync(fullAddress);
    }

    public VisicomMapPointProvider(HttpClient httpClient, IRequestOptions options) : base(httpClient, options)
    {
    }

}
