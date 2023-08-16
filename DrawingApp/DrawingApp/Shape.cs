using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    abstract class Shape
    {
        public abstract Color color { get; set; }
        public abstract void Draw(Drawing.ViewerForm viewer);
        public abstract bool IsIn(Point p);
        public abstract double GetArea();
    }
}
