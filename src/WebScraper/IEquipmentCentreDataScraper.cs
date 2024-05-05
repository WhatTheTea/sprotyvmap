namespace WhatTheTea.SprotyvMap.WebScraper
{
    public interface IEquipmentCentreDataScraper
    {
        Task<CentreData> Get(string district, int id);
        Task<IEnumerable<District>> GetAllDistricts();
    }
}
