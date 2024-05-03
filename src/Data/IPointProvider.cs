namespace WhatTheTea.SprotyvMap.Data
{
    public interface IPointProvider
    {
        EquipmentCentre GetPoint(int district, int id);
        List<EquipmentCentre> GetPointsByDistrict(int district);
        List<EquipmentCentre> GetAllPoints();
        
        Task<EquipmentCentre> GetPointAsync(int district, int id);
        Task<List<EquipmentCentre>> GetPointsByDistrictAsync(int district);
        Task<List<EquipmentCentre>> GetAllPointsAsync();
    }
}
