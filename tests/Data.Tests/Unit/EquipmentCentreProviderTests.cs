using RichardSzalay.MockHttp;
using WhatTheTea.SprotyvMap.Data;
using WhatTheTea.SprotyvMap.Data.Abstractions;

namespace WhatTheTea.SprotyvMap.Data.Tests.Unit;

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