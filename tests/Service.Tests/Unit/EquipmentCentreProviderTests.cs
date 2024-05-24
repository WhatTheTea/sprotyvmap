using FluentAssertions;
using RichardSzalay.MockHttp;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;
using WhatTheTea.SprotyvMap.SprotyvInUa;

namespace WhatTheTea.SprotyvMap.Service.Tests.Unit;

public class EquipmentCentreProviderTests
{
    private IEquipmentCentreProvider EquipmentCentreProvider { get; }
    
    public EquipmentCentreProviderTests()
    {
        var httpMock = new MockHttpMessageHandler();
        var httpClient = httpMock.ToHttpClient();
        // TODO: Mock responses
        IMapPointProvider mapPointProvider = new VisicomMapPointProvider(httpClient);
        IDataProvider dataProvider = WebScraper.Create(httpClient)
            .GetAwaiter().GetResult();
        EquipmentCentreProvider = new EquipmentCentreProvider(dataProvider, mapPointProvider);
    }

    [Fact]
    public async Task GetFirstEquipmentCentre()
    {
        var centre = await EquipmentCentreProvider.GetEquipmentCentreAsync(1, 1);

        centre.Title.Should().Be("Вінницький ОТЦК та СП");
        centre.Point.Should().BeEquivalentTo(new MapPoint(	49.23168, 28.443414));
    }
}