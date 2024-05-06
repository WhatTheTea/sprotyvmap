using WhatTheTea.SprotyvMap.WebScraper.Data;

namespace WhatTheTea.SprotyvMap.WebScraper
{
    public interface IEquipmentCentreDataScraper
    {
        Task<EquipmentCentre> GetCentreAsync(int districtId, int centreId);
        Task<IEnumerable<District>> GetAllDistrictsAsync();
    }
}
