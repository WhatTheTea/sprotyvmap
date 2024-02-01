using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportyvmap.Points
{
    public record Point(string Title, string Information, string Location, (double lat, double lng) Coordinates);
}
