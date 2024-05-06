using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using RichardSzalay.MockHttp;
using WhatTheTea.SprotyvMap.WebScraper;
using Xunit;

namespace WhatTheTea.SprotyvMap.WebScraper.Tests;

[TestSubject(typeof(SprotyvInUaScraper))]
public class SprotyvInUaScraperTest
{
    private const string SiteUri = "https://sprotyv.in.ua/";
    private const string TestSiteUri = "TestData/sprotyv.in.ua.htm";

    private SprotyvInUaScraper Scraper { get; set; }
    
    public SprotyvInUaScraperTest()
    {
        var httpMock = new MockHttpMessageHandler();
        var testPage = File.ReadAllText(TestSiteUri);
        httpMock.When(SiteUri)
            .Respond("text/html", testPage);
        var httpClient = httpMock.ToHttpClient();

        Scraper = new SprotyvInUaScraper(httpClient);
    }

    [Fact]
    public async Task FirstNodeIsVinnytsky()
    {
        var result = await Scraper.GetCentreAsync(1,1);
        
        result.Title.Should().Be("Вінницький ОТЦК та СП");
    }

    [Theory]
    [InlineData(0,0)]
    [InlineData(1,99)]
    [InlineData(99, 1)]
    public async Task OutOfBoundsNodes(int districtId, int centreId)
    {
        var act = async () => await Scraper.GetCentreAsync(districtId, centreId);

        await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    public async Task AllDistrictsFirstIsVinnytsky()
    {
        var result = await Scraper.GetAllDistrictsAsync();
        var district = result.First();
        var centre = district.EquipmentCentres.First();
        
        district.Name.Should().Be("")
    }
}