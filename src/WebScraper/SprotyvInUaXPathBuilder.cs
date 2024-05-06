namespace WhatTheTea.SprotyvMap.WebScraper;

internal class SprotyvInUaXPathBuilder
{
    private static string GetDistrictNameXPath(string district) =>
        $"/html/body/div/section[2]/div/div[{district}]/div/div[1]/span/span[1]/text()";
    
    public static string GetEquipmentCentreName(int districtId, int centreId) =>
        GetEquipmentCentreData(districtId, centreId, 1);
    public static string GetEquipmentCentreInfo(int districtId, int centreId) =>
        GetEquipmentCentreData(districtId, centreId, 2);
    public static string GetEquipmentCentreLocation(int districtId, int centreId) =>
        GetEquipmentCentreData(districtId, centreId, 3);
    private static string GetEquipmentCentreData(int districtId, int centreId, int columnId) =>
        GetTableXPath(districtId) + $"/tr[{centreId}]/td[{columnId}]/text()";
    public static string GetTableXPath(int districtId) =>
        $"/html/body/div/section[2]/div/div[{districtId}]/div/div[2]/div/div/table/tbody";



}