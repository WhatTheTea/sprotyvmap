using HtmlAgilityPack;
using WhatTheTea.SprotyvMap.Shared.Abstractions;
using WhatTheTea.SprotyvMap.Shared.Primitives;

namespace WhatTheTea.SprotyvMap.SprotyvInUa;

public class WebScraper : IDataProvider
{
    private const string SprotyvInUaUri = "https://sprotyv.in.ua/";
    
    private HttpClient HttpClient { get; set; }
    private HtmlDocument Document { get; set; }
    
    public static async Task<WebScraper> Create(HttpClient httpClient)
    {
        var instance = new WebScraper(httpClient);
        await instance.LoadDataAsync();
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
    public async Task LoadDataAsync()
    {
        var documentStream = await HttpClient.GetStreamAsync(SprotyvInUaUri);
        Document.Load(documentStream);
    }
    
    public IEnumerable<District> GetAllDistricts()
    {
        var districtsCount = SelectNode(XPathBuilder.AllDistrictsXPath())
            .ChildNodes.Count;
        for (int districtId = 1; districtId <= districtsCount; districtId++)
        {
            var centres = GetEquipmentCentres(districtId);
            var name = SelectNode(XPathBuilder.DistrictNameXPath(districtId)).InnerText;
            
            yield return new District(districtId,name,centres);
        }
    }

    private IEnumerable<EquipmentCentre> GetEquipmentCentres(int districtId)
    {
        var centresCount = SelectNode(XPathBuilder.DistrictXPath(districtId))
            .ChildNodes.Count;
        for (var centreId = 1; centreId < centresCount; centreId++)
        {
            yield return GetEquipmentCentre(districtId, centreId);
        }
    }

    public EquipmentCentre GetEquipmentCentre(int districtId, int centreId)
    {
        if (!IsValueInChildrenCountRange(
                SelectNode(XPathBuilder.AllDistrictsXPath()), districtId))
            throw new ArgumentOutOfRangeException(nameof(districtId));
        if (!IsValueInChildrenCountRange(
                SelectNode(XPathBuilder.DistrictXPath(districtId)), centreId))
            throw new ArgumentOutOfRangeException(nameof(centreId));
        
        return new EquipmentCentre(
            SelectCentreTitle(districtId, centreId),
            SelectCentreInformation(districtId, centreId),
            SelectCentreLocation(districtId, centreId),
            new MapPoint()
        );
    }

    private static bool IsValueInChildrenCountRange(HtmlNode node, int value)
    {
        var childNodesCount = node.ChildNodes.Count;
        return value <= childNodesCount && value >= 1;
    }

    private string SelectCentreTitle(int districtId, int centreId) =>
        SelectNode(XPathBuilder.EquipmentCentreName(districtId, centreId))?
            .InnerText ?? "";

    private string SelectCentreInformation(int districtId, int centreId) =>
        SelectNode(XPathBuilder.EquipmentCentreInfo(districtId, centreId))?
            .InnerText ?? "";

    private string SelectCentreLocation(int districtId, int centreId) =>
        SelectNode(XPathBuilder.EquipmentCentreLocation(districtId, centreId))?
            .InnerText ?? "";
    
    private HtmlNode SelectNode(string xpath) =>
        Document.DocumentNode
            .SelectSingleNode(xpath);

    public async Task<EquipmentCentre> GetEquipmentCentreAsync(int districtId, int centreId)
    {
        await LoadDataAsync();
        return GetEquipmentCentre(districtId, centreId);
    }

    public async Task<IEnumerable<District>> GetAllDistrictsAsync()
    {
        await LoadDataAsync();
        return GetAllDistricts();
    }
}