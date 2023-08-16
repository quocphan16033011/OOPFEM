using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    class Line : PolyLine
    {
        public Line(double x, double y, double z, double x1, double y1, double z1)
        {
            points = new Point[2];
            points[0] = new Point(new double[] { x, y, z });
            points[1] = new Point(new double[] { x1, y1, z1 });
        }
    }
}
