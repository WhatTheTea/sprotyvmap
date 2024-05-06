namespace Visicom.DataApi.Geocoder
{
    public interface IRequest
    {
        string Get();
        IRequest SetApiKey(string apiKey);
        IRequest SetLanguage(string language);
        IRequest SetCategories(string categories);
        IRequest ExcludeCategories(string categories);
        IRequest SetText(string search_text);
        IRequest SetWordText(string search_word);
        IRequest SetNear((double lat, double lng) coordinates);
        IRequest SetNear(string identifier);
        IRequest SetNearRadius(double radius);
        IRequest SetOrder(string order);
        IRequest SetIntersectionArea((double lat, double lng) coordinates);
        IRequest SetIntersectionArea(string identifier);
        IRequest SetContainsArea((double lat, double lng) coordinates);
        IRequest SetContainsArea(string identifier);
        IRequest SetZoom(int TMS);
        IRequest SetLimit(int limit);
        IRequest SetCountryCode(string code);
        IRequest SetCoutnryCodePriority(string code);
    }
}
