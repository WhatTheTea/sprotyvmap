using System.Net.Http.Json;

using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.WebApp.Services
{
    public class ApiEquipmentCentreProvider(HttpClient httpClient, string apiUrl = "https://sprotyvmap-api.azurewebsites.net") : IEquipmentCentreProvider
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly string apiUrl = apiUrl;
        private string apiDistrictsEndpoint => apiUrl + "api/Districts";

        public async Task<IEnumerable<District>> GetDistrictsAsync()
        {
            var response = await httpClient.GetFromJsonAsync<District[]>(apiDistrictsEndpoint);
            return response ?? [];
        }
    }
}
