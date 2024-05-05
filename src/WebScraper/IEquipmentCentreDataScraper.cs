namespace WhatTheTea.SprotyvMap.WebScraper
{
    public interface IEquipmentCentreDataScraper
    {
        Task<CentreData> GetCentreAsync(string district, int id);
        Task<IEnumerable<District>> GetAllDistrictsAsync();
    }
}
