namespace sprotyvmap.Geocoder
{
    public interface IGeocoderService
    {
        (double lat, double lng) GetCoordinates(string location);
    }
}
