using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Quadrilateral : Polygon
    {
        public Quadrilateral(double x, double y, double z, double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            points = new Point[4];
            points[0] = new Point(new double[] { x, y, z });
            points[1] = new Point(new double[] { x1, y1, z1 });
            points[2] = new Point(new double[] { x2, y2, z2 });
            points[3] = new Point(new double[] { x3, y3, z3 });
        }
    }
}
