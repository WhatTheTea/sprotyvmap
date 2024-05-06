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
    public async Task FirstEntryIsVinnytsky()
    {
        var result = await Scraper.GetCentreAsync(1,1);
        
        result.Title.Should().Be("Вінницький ОТЦК та СП");
    }
}