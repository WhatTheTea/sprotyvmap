using JetBrains.Annotations;

using System.Net.Http;

namespace WhatTheTea.SprotyvMap.WebScraper.Tests.Integration;

[TestSubject(typeof(SprotyvInUa.WebScraper))]
public class SprotyvInUaScraperTests : SprotyvInUaTestsBase
{
    public SprotyvInUaScraperTests()
    {
        var httpClient = new HttpClient();

        Scraper = SprotyvInUa.WebScraper.Create(httpClient)
            .GetAwaiter().GetResult();
    }
}