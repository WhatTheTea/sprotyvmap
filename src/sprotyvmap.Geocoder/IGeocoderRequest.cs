using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprotyvmap.Geocoder
{
    public interface IGeocoderRequest
    {
        string GetJSON();
        string GetCSV();
    }
}
