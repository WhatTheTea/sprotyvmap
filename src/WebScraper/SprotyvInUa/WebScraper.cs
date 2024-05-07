using System.Diagnostics.CodeAnalysis;
using HtmlAgilityPack;
using WhatTheTea.SprotyvMap.WebScraper.Data;

namespace WhatTheTea.SprotyvMap.WebScraper.SprotyvInUa;

public class WebScraper : IEquipmentCentreDataScraper
{
    private WebScraper(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public static async Task<WebScraper> Create(HttpClient httpClient)
    {
        var instance = new WebScraper(httpClient);
        await instance.LoadHtmlAsync();
        return instance;
    }

    private const string SprotyvInUaUri = "https://sprotyv.in.ua/";
    private HttpClient HttpClient { get; set; }
    [NotNull]
    private HtmlDocument? Document { get; set; }

    /// <summary>
    /// Downloads sprotyv.in.ua. Can be used to update data.
    /// </summary>
    public async Task LoadHtmlAsync()
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
        await LoadHtmlAsync();
        
        var allDistrictsNode = SelectNode(XPathBuilder.GetAllDistrictsXPath());
        var districtsCount = allDistrictsNode.ChildNodes.Count;
        if (districtId > districtsCount || districtId < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(districtId));
        }

        var districtNode = SelectNode(XPathBuilder.GetDistrictXPath(districtId));
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