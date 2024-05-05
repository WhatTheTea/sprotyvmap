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

        this.Scraper = SprotyvInUaScraper.CreateAsync(httpClient)
            .GetAwaiter()
            .GetResult();
    }

    [Fact]
    public async Task FirstEntryIsVinnytsky()
    {
        var result = await Scraper.GetAllDistrictsAsync();
        var district = result.First();
        var centre = district.EquipmentCentres.First();

        district.Name.Should().Be("Вінницька область");
        centre.Title.Should().Be("Вінницький ОТЦК та СП");
    }
}