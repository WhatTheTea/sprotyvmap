namespace WhatTheTea.SprotyvMap.WebScraper;

public class District
{
    public string Name { get; set; }
    public IEnumerable<CentreData> EquipmentCentres { get; set; }
}