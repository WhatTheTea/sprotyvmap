namespace WhatTheTea.SprotyvMap.Data.Abstractions
{
    public interface IEquipmentCentreProvider
    {
        EquipmentCentre GetEquipmentCentre(int district, int id);
        List<EquipmentCentre> GetEquipmentCentresByDistrict(int district);
        List<EquipmentCentre> GetAllEquipmentCentres();
        
        Task<EquipmentCentre> GetEquipmentCentreAsync(int district, int id);
        Task<List<EquipmentCentre>> GetEquipmentCentresByDistrictAsync(int district);
        Task<List<EquipmentCentre>> GetAllEquipmentCentresAsync();
    }
}
