using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprotyvmap.Geocoder
{
    public interface IGeocoderRequestBuilder
    {
        /// <summary>
        /// Метод отримання результату білдера
        /// </summary>
        /// <returns><see cref="IGeocoderRequest"/> Запит до API геокодера готовий до виконання</returns>
        IGeocoderRequest GetRequest();
        IGeocoderRequestBuilder SetApiKey(string apiKey);
        IGeocoderRequestBuilder SetLanguage(ELanguage language);
        IGeocoderRequestBuilder SetCategories(IEnumerable<ECategories> categories);
        IGeocoderRequestBuilder ExcludeCategories(IEnumerable<ECategories> categories);
        IGeocoderRequestBuilder SetText(string search_text);
        IGeocoderRequestBuilder SetWordText(string search_word);
        IGeocoderRequestBuilder SetNear((double lat, double lng) coordinates);
        IGeocoderRequestBuilder SetNear(string identifier);
        IGeocoderRequestBuilder SetNearRadius(double radius);
        IGeocoderRequestBuilder SetOrder(EOrder order);
        IGeocoderRequestBuilder SetIntersectionArea((double lat, double lng) coordinates);
        IGeocoderRequestBuilder SetIntersectionArea(string identifier);
        IGeocoderRequestBuilder SetContainsArea((double lat, double lng) coordinates);
        IGeocoderRequestBuilder SetContainsArea(string identifier);
        IGeocoderRequestBuilder SetZoom(int TMS);
        IGeocoderRequestBuilder SetLimit(int limit);
        IGeocoderRequestBuilder SetCountryCode(ECountryCodes code);
        IGeocoderRequestBuilder SetCoutnryCodePriority(ECountryCodes code);
    }
}
