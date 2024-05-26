using System;
using System.Net.Http;
using Visicom.DataApi.Geocoder.Enums;

namespace Visicom.DataApi.Geocoder.Tests.Integration;

public class RequestTests : RequestTestsBase
{
    public RequestTests()
    {
        var apikey = Environment.GetEnvironmentVariable("VISICOM_DAPI_KEY");
        var options = new RequestOptions(Languages.Ukrainian, apikey ?? string.Empty);
        Geocoder = new BasicGeocoder(new HttpClient(), options);
    }
}