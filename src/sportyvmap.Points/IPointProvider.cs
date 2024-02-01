using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportyvmap.Points
{
    public interface IPointProvider
    {
        Point GetPoint(int district, int id);
        List<Point> GetPointsByDistrict(int district);
        List<Point> GetAllPoints();
    }
}
