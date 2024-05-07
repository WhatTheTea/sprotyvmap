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

[TestSubject(typeof(SprotyvInUa.WebScraper))]
public class SprotyvInUaScraperTest
{
    private const string SiteUri = "https://sprotyv.in.ua/";
    private const string TestSiteUri = "TestData/sprotyv.in.ua.htm";

    private SprotyvInUa.WebScraper Scraper { get; set; }
    
    public SprotyvInUaScraperTest()
    {
        var httpMock = new MockHttpMessageHandler();
        var testPage = File.ReadAllText(TestSiteUri);
        httpMock.When(SiteUri)
            .Respond("text/html", testPage);
        var httpClient = httpMock.ToHttpClient();

        Scraper = SprotyvInUa.WebScraper.Create(httpClient)
            .GetAwaiter().GetResult();
    }

    [Fact]
    public void FirstNodeIsVinnytsky()
    {
        var result = Scraper.GetEquipmentCentre(1,1);
        
        result.Title.Should().Be("Вінницький ОТЦК та СП");
    }

    [Theory]
    [InlineData(0,0)]
    [InlineData(1,99)]
    [InlineData(99, 1)]
    public void OutOfBoundsNodes(int districtId, int centreId)
    {
        var act = () => Scraper.GetEquipmentCentre(districtId, centreId);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void AllDistrictsFirstIsVinnytsky()
    {
        var result = Scraper.GetAllDistricts();
        var district = result.First();
        var centre = district.EquipmentCentres.First();

        district.Name.Should().Be("Вінницька область");
        centre.Title.Should().Be("Вінницький ОТЦК та СП");
    }
}