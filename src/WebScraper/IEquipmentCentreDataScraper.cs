using WhatTheTea.SprotyvMap.WebScraper.Data;

namespace WhatTheTea.SprotyvMap.WebScraper
{
    public interface IEquipmentCentreDataScraper
    {
        EquipmentCentre GetEquipmentCentre(int districtId, int centreId);
        IEnumerable<District> GetAllDistricts();
    }
}
