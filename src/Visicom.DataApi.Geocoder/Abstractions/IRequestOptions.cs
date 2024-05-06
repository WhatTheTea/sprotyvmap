namespace Visicom.DataApi.Geocoder.Abstractions;

public interface IRequestOptions
{
    public string ApiKey { get; }
    public string Language { get; }
    public IEnumerable<string> Categories { get; }
    public IEnumerable<string> ExcludeCategories { get; }
    public string Text { get; }
    public string WordText { get; }
    public string Near { get; }
    public string NearRadius { get; }
    public string Order { get; }
    public string IntersectionArea { get; }
    public string ContainsArea { get; }
    public string Zoom { get; }
    public string Limit { get; }
    public string CountryCode { get; }
    public string CountryCodePriority { get; }
}