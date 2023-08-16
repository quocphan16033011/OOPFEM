using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    internal class Point
    {
        private double[] location;
        public float Size { get; set; }
        public Color Color { get; set; }
        public Point(double[] location)
        {
            this.location = location;
            Size = 5;
            Color = Color.Red;
        }
        public double GetLocation(int indexCoordinate)
        {
            return location[indexCoordinate];
        }
        public void SetLocation(int indexCoordinate, double value)
        {
            
        }
        public void Move(double dx, double dy, double dz)
        {
            location[0] += dx;
            location[1] += dy;
            location[2] += dz;
        }
        public double GetDistance(Point point2)
        {
            double distance = 0;
            for (int i = 0; i < location.Length; i++)
            {
                distance += Math.Pow((location[i] - point2.location[i]), 2);
            }
            return Math.Sqrt(distance);
        }
        public void Draw(Drawing.ViewerForm viewer)
        {
            Drawing.Point p = new Drawing.Point(location[0], location[1], location[2]);
            p.SetPointSize(Size);
            p.SetColor(Color);
            viewer.AddObject3D(p);
        }
        public double[] getVector(Point p)
        {
            return new double[] {p.location[0]-location[0],p.location[1]- location[1], p.location[2] - location[2] };
        }
    }
}
