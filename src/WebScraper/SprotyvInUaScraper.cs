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
        
        var allDistrictsNode = SelectNode(SprotyvInUaXPathBuilder.GetAllDistrictsXPath());
        var districtsCount = allDistrictsNode.ChildNodes.Count;
        if (districtId > districtsCount || districtId < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(districtId));
        }

        var districtNode = SelectNode(SprotyvInUaXPathBuilder.GetDistrictXPath(districtId));
        var centresCount = districtNode.ChildNodes.Count;
        if (centreId > centresCount || centreId < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(centreId));
        }
        
        return new EquipmentCentre(
            SelectCentreTitle(districtId, centreId),
            SelectCentreInformation(districtId, centreId),
            SelectCentreLocation(districtId, centreId)
            );
    }

    private string SelectCentreTitle(int districtId, int centreId) =>
        SelectNode(SprotyvInUaXPathBuilder.GetEquipmentCentreName(districtId, centreId))
            .InnerText;

    private string SelectCentreInformation(int districtId, int centreId) =>
        SelectNode(SprotyvInUaXPathBuilder.GetEquipmentCentreInfo(districtId, centreId))
            .InnerText;

    private string SelectCentreLocation(int districtId, int centreId) =>
        SelectNode(SprotyvInUaXPathBuilder.GetEquipmentCentreLocation(districtId, centreId))
            .InnerText;
    
    private HtmlNode SelectNode(string xpath) =>
        Document.DocumentNode
            .SelectSingleNode(xpath);

}