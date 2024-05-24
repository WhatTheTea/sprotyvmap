namespace WhatTheTea.SprotyvMap.SprotyvInUa;

internal static class XPathBuilder
{
    public static string DistrictNameXPath(int districtId) =>
        $"{AllDistrictsXPath()}/div[{districtId}]/div/div[1]/span/span[1]/text()";
    
    public static string EquipmentCentreName(int districtId, int centreId) =>
        EquipmentCentreData(districtId, centreId, 1);
    public static string EquipmentCentreInfo(int districtId, int centreId) =>
        EquipmentCentreData(districtId, centreId, 2);
    public static string EquipmentCentreLocation(int districtId, int centreId) =>
        EquipmentCentreData(districtId, centreId, 3);
    private static string EquipmentCentreData(int districtId, int centreId, int columnId) =>
        DistrictXPath(districtId) + $"/tr[{centreId}]/td[{columnId}]/text()";
    
    public static string DistrictXPath(int districtId) =>
        AllDistrictsXPath() + $"/div[{districtId}]/div/div[2]/div/div/table/tbody";
    
    public static string AllDistrictsXPath() => "/html/body/div/section[2]/div";



}