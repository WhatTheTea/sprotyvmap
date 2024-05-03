namespace Visicom.DataApi.Geocoder
{
    public interface IGeocoderService
    {
        (double lat, double lng) GetCoordinates(string location);
    }
}
