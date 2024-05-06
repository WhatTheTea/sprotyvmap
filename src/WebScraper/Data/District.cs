namespace WhatTheTea.SprotyvMap.WebScraper.Data;

public class District
{
    public string Name { get; set; }
    public IEnumerable<EquipmentCentre> EquipmentCentres { get; set; }
}