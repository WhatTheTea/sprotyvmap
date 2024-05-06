using System.Diagnostics.CodeAnalysis;
using Visicom.DataApi.Geocoder.Abstractions;
using WhatTheTea.SprotyvMap.Primitives;

namespace Visicom.DataApi.Geocoder;

public class BasicGeocoder : IGeocoder
{
    private IRequestOptions _options;

    public BasicGeocoder(IRequestOptions options)
    {
        _options = options;
    }

    public void SetOptions(IRequestOptions options) => _options = options;

    public MapPoint GetCoordinates(string searchTerm, bool isByWholeWord = false)
        => GetCoordinatesAsync(searchTerm, isByWholeWord)
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public Task<MapPoint> GetCoordinatesAsync(string searchTerm, bool isByWholeWord = false)
    {
        throw new NotImplementedException();
    }
}