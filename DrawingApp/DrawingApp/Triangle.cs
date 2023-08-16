using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    class Triangle : Polygon
    {
        public Triangle(double x, double y, double z, double x1, double y1, double z1, double x2, double y2, double z2) 
        {
            points = new Point[3];
            points[0] = new Point(new double[] { x, y, z });
            points[1] = new Point(new double[] { x1, y1, z1 });
            points[2] = new Point(new double[] { x2, y2, z2 });
        }
    }
}
