using HtmlAgilityPack;

namespace WhatTheTea.SprotyvMap.WebScraper;

public class SprotyvInUaScraper : IEquipmentCentreDataScraper
{
    private const string SprotyvInUaUri = "https://sprotyv.in.ua/";
    private HttpClient HttpClient { get; set; }
    private HtmlDocument Document { get; set; }
    
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
        var documentStream = await this.HttpClient.GetStreamAsync(SprotyvInUaUri);
        var document = new HtmlDocument();
        document.Load(documentStream);
        this.Document = document;
    }
    
    public Task<CentreData> GetCentreAsync(string district, int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<District>> GetAllDistrictsAsync()
    {
        throw new NotImplementedException();
    }
}