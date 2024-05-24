using RichardSzalay.MockHttp;
using WhatTheTea.SprotyvMap.Shared.Abstractions;

namespace WhatTheTea.SprotyvMap.Service.Tests.Unit;

public class EquipmentCentreProviderTests
{
    private IEquipmentCentreProvider EquipmentCentreProvider { get; }
    
    public EquipmentCentreProviderTests()
    {
        var httpMock = new MockHttpMessageHandler();
        var httpClient = httpMock.ToHttpClient();
        IMapPointProvider mapPointProvider = new VisicomMapPointProvider(httpClient);
        IDataProvider dataProvider = new SprotyvInUaDataProvider(httpClient);
        EquipmentCentreProvider = new EquipmentCentreProvider(dataProvider, mapPointProvider);
    }
}