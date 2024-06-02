using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.WebApp.Services
{
    public class ApiEquipmentCentreProvider(HttpClient httpClient) : IEquipmentCentreProvider
    {
        private readonly HttpClient httpClient = httpClient;

        public Task<IEnumerable<District>> GetDistrictsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
