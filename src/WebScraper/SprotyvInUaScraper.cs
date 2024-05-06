using System.Diagnostics.CodeAnalysis;
using HtmlAgilityPack;
using WhatTheTea.SprotyvMap.WebScraper.Data;

namespace WhatTheTea.SprotyvMap.WebScraper;

public class SprotyvInUaScraper(HttpClient httpClient) : IEquipmentCentreDataScraper
{
    private const string SprotyvInUaUri = "https://sprotyv.in.ua/";
    private HttpClient HttpClient { get; set; } = httpClient;
    [NotNull]
    private HtmlDocument? Document { get; set; }

    private async Task DownloadHtmlAsync()
    {
        var documentStream = await HttpClient.GetStreamAsync(SprotyvInUaUri);
        var document = new HtmlDocument();
        document.Load(documentStream);
        Document = document;
    }
    
    public Task<IEnumerable<District>> GetAllDistrictsAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<EquipmentCentre> GetCentreAsync(int districtId, int centreId)
    {
        await DownloadHtmlAsync();
        
        var centre = new EquipmentCentre(
            SelectCentreTitle(districtId, centreId),
            SelectCentreInformation(districtId, centreId),
            SelectCentreLocation(districtId, centreId)
            );

        return centre;
    }

    private string SelectCentreTitle(int districtId, int centreId) =>
        SelectNode(SprotyvInUaXPathBuilder.GetEquipmentCentreName(districtId, centreId));

    private string SelectCentreInformation(int districtId, int centreId) =>
        SelectNode(SprotyvInUaXPathBuilder.GetEquipmentCentreInfo(districtId, centreId));

    private string SelectCentreLocation(int districtId, int centreId) =>
        SelectNode(SprotyvInUaXPathBuilder.GetEquipmentCentreLocation(districtId, centreId));
    
    private string SelectNode(string xpath) =>
        Document.DocumentNode
            .SelectSingleNode(xpath).InnerText;

}