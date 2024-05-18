using System.Diagnostics.CodeAnalysis;
using System.Text;
using Visicom.DataApi.Geocoder.Abstractions;
using Visicom.DataApi.Geocoder.Enums;
using Visicom.DataApi.Geocoder.Extensions;
using WhatTheTea.SprotyvMap.Primitives;

namespace Visicom.DataApi.Geocoder;

public class BasicGeocoder : IGeocoder
{
    private const string requestBase = "https://api.visicom.ua/data-api/5.0/";
    
    private IRequestOptions _options;
    private HttpClient _httpClient;

    public BasicGeocoder(HttpClient httpClient, IRequestOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public void SetOptions(IRequestOptions options) => _options = options;

    public MapPoint GetCoordinates(string searchTerm, bool isByWholeWord = false)
        => GetCoordinatesAsync(searchTerm, isByWholeWord)
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();

    public Task<MapPoint> GetCoordinatesAsync(string searchTerm, bool isByWholeWord = false)
    {
        var requestUrl = BuildRequestUrl(searchTerm, isByWholeWord);
        throw new NotImplementedException();
    }

    private string BuildRequestUrl(string searchTerm, bool isByWholeWord)
    {
        var requestBuilder = new StringBuilder(requestBase);
        
        requestBuilder.AppendEndpoint(_options.Language.ToRequestString())
            .AppendEndpoint("geocode.json")
            .AppendParam("key", _options.ApiKey, true)
            .AppendParam(isByWholeWord ? "wt" : "t", searchTerm);
        AppendOptions(requestBuilder);
        
        return requestBuilder.ToString();
    }

    private void AppendOptions(StringBuilder requestBuilder)
    {
        var categoriesString = string.Join(',', _options.Categories.Select(x => x.ToRequestString()));
        var excludeCategoriesString = string.Join(',', _options.ExcludeCategories.Select(x => x.ToRequestString()));

        requestBuilder.AppendParam("ci", categoriesString)
            .AppendParam("ce", excludeCategoriesString)
            .AppendParam("n", _options.Near)
            .AppendParam("r", _options.NearRadius)
            .AppendParam("order", _options.Order.ToRequestString())
            .AppendParam("i", _options.IntersectionArea)
            .AppendParam("co", _options.ContainsArea)
            .AppendParam("z", _options.Zoom)
            .AppendParam("l", _options.Limit)
            .AppendParam("c", _options.CountryCode)
            .AppendParam("bc", _options.CountryCodePriority);
    }
}