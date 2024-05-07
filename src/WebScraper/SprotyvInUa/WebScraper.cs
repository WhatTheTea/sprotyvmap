using System.Diagnostics.CodeAnalysis;
using HtmlAgilityPack;
using WhatTheTea.SprotyvMap.WebScraper.Data;

namespace WhatTheTea.SprotyvMap.WebScraper.SprotyvInUa;

public class WebScraper : IEquipmentCentreDataScraper
{
    private const string SprotyvInUaUri = "https://sprotyv.in.ua/";
    
    private HttpClient HttpClient { get; set; }
    private HtmlDocument Document { get; set; }
    
    public static async Task<WebScraper> Create(HttpClient httpClient)
    {
        var instance = new WebScraper(httpClient);
        await instance.LoadHtmlAsync();
        return instance;
    }
    
    private WebScraper(HttpClient httpClient)
    {
        HttpClient = httpClient;
        Document = new HtmlDocument();
    }

    /// <summary>
    /// Downloads sprotyv.in.ua. Can be used to update data.
    /// </summary>
    public async Task LoadHtmlAsync()
    {
        var documentStream = await HttpClient.GetStreamAsync(SprotyvInUaUri);
        Document.Load(documentStream);
    }
    
    public Task<IEnumerable<District>> GetAllDistrictsAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<EquipmentCentre> GetCentreAsync(int districtId, int centreId)
    {
        await LoadHtmlAsync();

        if (!IsValueInChildrenCountRange(
                SelectNode(XPathBuilder.GetAllDistrictsXPath()), districtId))
            throw new ArgumentOutOfRangeException(nameof(districtId));
        if (!IsValueInChildrenCountRange(
                SelectNode(XPathBuilder.GetDistrictXPath(districtId)), centreId))
            throw new ArgumentOutOfRangeException(nameof(centreId));
        
        return new EquipmentCentre(
                SelectCentreTitle(districtId, centreId),
                SelectCentreInformation(districtId, centreId),
                SelectCentreLocation(districtId, centreId)
                );
    }

    private static bool IsValueInChildrenCountRange(HtmlNode node, int value)
    {
        var childNodesCount = node.ChildNodes.Count;
        return value <= childNodesCount && value >= 1;
    }

    private string SelectCentreTitle(int districtId, int centreId) =>
        SelectNode(XPathBuilder.GetEquipmentCentreName(districtId, centreId))
            .InnerText;

    private string SelectCentreInformation(int districtId, int centreId) =>
        SelectNode(XPathBuilder.GetEquipmentCentreInfo(districtId, centreId))
            .InnerText;

    private string SelectCentreLocation(int districtId, int centreId) =>
        SelectNode(XPathBuilder.GetEquipmentCentreLocation(districtId, centreId))
            .InnerText;
    
    private HtmlNode SelectNode(string xpath) =>
        Document.DocumentNode
            .SelectSingleNode(xpath);

}