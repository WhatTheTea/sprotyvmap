namespace Visicom.DataApi.Geocoder
{
    public interface IGeocoderRequest
    {
        string Get();
        IGeocoderRequest SetApiKey(string apiKey);
        IGeocoderRequest SetLanguage(string language);
        IGeocoderRequest SetCategories(string categories);
        IGeocoderRequest ExcludeCategories(string categories);
        IGeocoderRequest SetText(string search_text);
        IGeocoderRequest SetWordText(string search_word);
        IGeocoderRequest SetNear((double lat, double lng) coordinates);
        IGeocoderRequest SetNear(string identifier);
        IGeocoderRequest SetNearRadius(double radius);
        IGeocoderRequest SetOrder(string order);
        IGeocoderRequest SetIntersectionArea((double lat, double lng) coordinates);
        IGeocoderRequest SetIntersectionArea(string identifier);
        IGeocoderRequest SetContainsArea((double lat, double lng) coordinates);
        IGeocoderRequest SetContainsArea(string identifier);
        IGeocoderRequest SetZoom(int TMS);
        IGeocoderRequest SetLimit(int limit);
        IGeocoderRequest SetCountryCode(string code);
        IGeocoderRequest SetCoutnryCodePriority(string code);
    }
}
