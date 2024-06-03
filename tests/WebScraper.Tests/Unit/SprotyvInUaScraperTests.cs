using JetBrains.Annotations;

using RichardSzalay.MockHttp;

using System.IO;
using System.Net.Mime;

namespace WhatTheTea.SprotyvMap.WebScraper.Tests.Unit;

[TestSubject(typeof(SprotyvInUa.WebScraper))]
public class SprotyvInUaScraperTests : SprotyvInUaTestsBase
{
    private const string SiteUri = "https://sprotyv.in.ua/";
    private const string TestSiteUri = "TestData/sprotyv.in.ua.htm";

    public SprotyvInUaScraperTests()
    {
        var httpMock = new MockHttpMessageHandler();
        var testPage = File.ReadAllText(TestSiteUri);
        httpMock.When(SiteUri)
            .Respond(MediaTypeNames.Text.Html, testPage);
        var httpClient = httpMock.ToHttpClient();

        Scraper = SprotyvInUa.WebScraper.Create(httpClient)
            .GetAwaiter().GetResult();
    }
}