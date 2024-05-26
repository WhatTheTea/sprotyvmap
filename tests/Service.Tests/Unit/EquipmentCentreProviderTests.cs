using System.Net.Mime;
using System.Web;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Visicom.DataApi.Geocoder;
using Visicom.DataApi.Geocoder.Enums;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;
using WhatTheTea.SprotyvMap.SprotyvInUa;

namespace WhatTheTea.SprotyvMap.Service.Tests.Unit;

public class EquipmentCentreProviderTests
{
    private const string VinnytskyOtccAddress = "м. Вінниця, вул. Данила Галицького, 31";
    private static string VinnytskyOtccRequest =>
        "https://api.visicom.ua/data-api/5.0/uk/geocode.json?key=APIKEY&t=" 
        + HttpUtility.UrlEncode(VinnytskyOtccAddress)
        + "&order=relevance";
    private const string VinnytskyOtccResponsePath = "TestData/geocode.json";
    private const string SprotyvInUaUrl = "https://sprotyv.in.ua/";
    private const string SprotyvInUaPath = "TestData/sprotyv.in.ua.htm";
    
    private IEquipmentCentreProvider EquipmentCentreProvider { get; }
    
    public EquipmentCentreProviderTests()
    {
        var httpMock = new MockHttpMessageHandler();
        MockSprotyvInUa(httpMock);
        MockVisicomDataApi(httpMock);
        var httpClient = httpMock.ToHttpClient();

        var requestOptions = new RequestOptions(Languages.Ukrainian, "APIKEY");
        IMapPointProvider mapPointProvider = new VisicomMapPointProvider(httpClient, requestOptions);
        IDataProvider dataProvider = WebScraper.Create(httpClient)
            .GetAwaiter().GetResult();
        
        EquipmentCentreProvider = new EquipmentCentreProvider(dataProvider, mapPointProvider);
    }

    private static void MockVisicomDataApi(MockHttpMessageHandler httpMock)
    {
        var response = File.ReadAllText(VinnytskyOtccResponsePath);
        httpMock.When(VinnytskyOtccRequest)
            .Respond(MediaTypeNames.Application.Json, response);
    }

    private static void MockSprotyvInUa(MockHttpMessageHandler httpMock)
    {
        var sprotyvInUa = File.ReadAllText(SprotyvInUaPath);
        httpMock.When(SprotyvInUaUrl)
            .Respond(MediaTypeNames.Text.Html, sprotyvInUa);
    }

    [Fact]
    public async Task GetFirstEquipmentCentre()
    {
        var centre = await EquipmentCentreProvider.GetEquipmentCentreAsync(1, 1);

        centre.Title.Should().Be("Вінницький ОТЦК та СП");
        centre.Point.Should().BeEquivalentTo(new MapPoint(	49.23168, 28.443414));
    }
}