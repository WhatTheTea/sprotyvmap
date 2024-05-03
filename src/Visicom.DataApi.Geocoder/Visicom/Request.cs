namespace Visicom.DataApi.Geocoder.Visicom;

public class Request : IGeocoderRequest
{
    public string Get()
    {
        throw new NotImplementedException();
    }
    public IGeocoderRequest ExcludeCategories(string categories)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetApiKey(string apiKey)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetCategories(string categories)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetContainsArea((double lat, double lng) coordinates)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetContainsArea(string identifier)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetCountryCode(string code)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetCoutnryCodePriority(string code)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetIntersectionArea((double lat, double lng) coordinates)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetIntersectionArea(string identifier)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetLanguage(string language)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetLimit(int limit)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetNear((double lat, double lng) coordinates)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetNear(string identifier)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetNearRadius(double radius)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetOrder(string order)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetText(string search_text)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetWordText(string search_word)
    {
        throw new NotImplementedException();
    }

    public IGeocoderRequest SetZoom(int TMS)
    {
        throw new NotImplementedException();
    }
}
