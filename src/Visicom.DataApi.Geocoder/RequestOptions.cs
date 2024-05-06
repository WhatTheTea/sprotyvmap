using Visicom.DataApi.Geocoder.Abstractions;

namespace Visicom.DataApi.Geocoder;

public class RequestOptions(string language, string apikey) : IRequestOptions
{
    public string ApiKey
    {
        get => apikey;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value was empty");
            }

            apikey = value;
        }
    }

    public string Language
    {
        get => language;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value was empty");
            }

            language = value;
        }
    }

    public IEnumerable<string> Categories { get; set; } = [];
    public IEnumerable<string> ExcludeCategories { get; set; } = [];
    public string Text { get; set; } = string.Empty;
    public string WordText { get; set; } = string.Empty;
    public string Near { get; set; } = string.Empty;
    public string NearRadius { get; set; } = string.Empty;
    public string Order { get; set; } = string.Empty;
    public string IntersectionArea { get; set; } = string.Empty;
    public string ContainsArea { get; set; } = string.Empty;
    public string Zoom { get; set; } = string.Empty;
    public string Limit { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string CountryCodePriority { get; set; } = string.Empty;
}