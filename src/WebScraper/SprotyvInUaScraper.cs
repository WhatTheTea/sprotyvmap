using HtmlAgilityPack;

namespace WhatTheTea.SprotyvMap.WebScraper;

public class SprotyvInUaScraper : IEquipmentCentreDataScraper
{
    private const string SprotyvInUaUri = "https://sprotyv.in.ua/";
    private HtmlDocument Document { get; set; }
    private HttpClient HttpClient { get; set; }
    
    private SprotyvInUaScraper(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public static async Task<SprotyvInUaScraper> CreateAsync(HttpClient client)
    {
        var instance = new SprotyvInUaScraper(client);
        await instance.SetDocumentAsync();
        
        return instance;
    }

    private async Task SetDocumentAsync()
    {
        var rawDocument = await this.HttpClient.GetStringAsync(SprotyvInUaUri);
        var document = new HtmlDocument();
        document.Load(rawDocument);
        this.Document = document;
    }
    
    public Task<CentreData> Get(string district, int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<District>> GetAllDistricts()
    {
        throw new NotImplementedException();
    }
}