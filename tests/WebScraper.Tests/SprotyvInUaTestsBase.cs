using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace WhatTheTea.SprotyvMap.WebScraper.Tests;

public abstract class SprotyvInUaTestsBase
{
    protected SprotyvInUa.WebScraper Scraper { get; set; }

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