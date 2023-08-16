using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    internal class PolyLine
    {
        protected Point[] points;
        public float Size { get; set; }
        public Color Color { get; set; }
        public bool IsDrawPoints { get; set; }
        public PolyLine(params Point[] points)
        {
            this.points = points;
            Size = 2;
            Color = Color.Green;
            IsDrawPoints = true;
        }
        public Point GetPoint(int indexPoint)
        {
            return points[indexPoint];
        }
        public void SetEndPoint(int indexPoint, Point p1)
        {
            points[indexPoint] = p1;
        }
        public int CountPoints()
        {
            return points.Length;
        }
        public double GetLength()
        {
            double length = 0;
            for(int i = 0; i < CountPoints() - 1; i++)
            {
                length += points[i].GetDistance(points[i + 1]);
            }
            return length; 

        }
        public void Draw(Drawing.ViewerForm viewer)
        {
            if (IsDrawPoints)
            {
                for (int i = 0; i < CountPoints(); i++)
                {
                    points[i].Draw(viewer);
                }
            }
                 for (int i = 0; i < CountPoints()-1; i++)
            {
                Drawing.Geometry.Line line = new Drawing.Geometry.Line(points[i].GetLocation(0), points[i].GetLocation(1), points[i].GetLocation(2), points[i+1].GetLocation(0), points[i + 1].GetLocation(1), points[i + 1].GetLocation(2));
                line.SetWidth(Size);
                line.SetColor(Color);
                viewer.AddObject3D(line);
            }
        }
    }
}
