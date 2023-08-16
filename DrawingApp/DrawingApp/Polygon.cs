using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    class Polygon : Shape
    {
        protected Point[] points;
        public bool IsDrawPoint { get; set; }
        public Color ColorPoints { get; set; }
        public float SizePoints { get; set; }
        public float SizeLines { get; set; }
        public bool IsDrawLine { get; set; }
        public Color ColorLines { get; set; }
        public Polygon(params Point[] points)
        {
            this.points = points;
            Color = Color.Green;
            SizePoints = 5;
            ColorPoints = Color.Orange;
            IsDrawPoint = true;
            IsDrawLine = true;
            ColorLines = Color.Red;
            SizeLines = 5;
        }

        public Point GetPoint(int indexPoint)
        {
            return points[indexPoint - 1];
        }
        public void SetPoint(int indexPoint, Point point)
        {
            points[indexPoint - 1] = point;
        }
        public int CountPoints()
        {
            return points.Length;
        }
        public Point GetEndPoint()
        {
            return points[points.Length - 1];
        }
        public void SetEndPoint(Point point)
        {
            points[points.Length - 1] = point;
        }
        public override double GetArea()
        {
            double result = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                result += 1 / 2.0 * (points[i].GetLocation(0) * points[i + 1].GetLocation(1)- points[i].GetLocation(1) * points[i + 1].GetLocation(0));
            }
            result += 1 / 2.0 * (points[CountPoints()-1].GetLocation(0) * points[0].GetLocation(1) - points[CountPoints() - 1].GetLocation(1) * points[0].GetLocation(0));
            return result;
        }
        public override bool IsIn(Point point)
        {
            double z0 = 0;
            double[] vec1 = new double[3];
            double[] vec2 = new double[3];
            double z1 = 0;
            for (int i = 0; i < CountPoints()-1; i++)
            {
                vec1 = points[i].getVector(points[i + 1]);
                vec2 = points[i].getVector(point);         
                if (i == 0)
                {
                    z0 = vec1[0] * vec2[1] - vec1[1] * vec2[0];
                } else
                {
                    z1 = vec1[0] * vec2[1] - vec1[1] * vec2[0];
                    if (z1*z0 < 0)
                    {
                        return false;
                    }
                }
            }
            vec1 = points[CountPoints()-1].getVector(points[0]);
            vec2 = points[CountPoints() - 1].getVector(point);
            z1 = vec1[0] * vec2[1] - vec1[1] * vec2[0];
            if (z1 * z0 < 0)
            {
                return false;
            }
            return true;
        }
        public override void Draw(Drawing.ViewerForm viewer)
        {
            if (IsDrawPoint)
            {
                for (int i = 0; i < CountPoints(); i++)
                {
                    points[i].Draw(viewer);
                    points[1].Color = ColorPoints;
                    points[i].Size = SizePoints;
                }
            }
            double[] x = new double[points.Length];
            double[] y = new double[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                x[i] = points[i].GetLocation(0);
                y[i] = points[i].GetLocation(1);
            }
            if (IsDrawLine)
            {
                for (int i = 0; i < points.Length - 1; i++)
                {
                    Drawing.Geometry.Line line = new Drawing.Geometry.Line(
                        points[i].GetLocation(0),
                        points[i].GetLocation(1),
                        points[i].GetLocation(2),
                        points[i + 1].GetLocation(0),
                        points[i + 1].GetLocation(1),
                        points[i + 1].GetLocation(2)
                        );
                    line.SetWidth(SizeLines);
                    line.SetColor(ColorLines);
                    viewer.AddObject3D(line);
                }
                Drawing.Geometry.Line last = new Drawing.Geometry.Line(
                        points[CountPoints() - 1].GetLocation(0),
                        points[CountPoints() - 1].GetLocation(1),
                        points[CountPoints() - 1].GetLocation(2),
                        points[0].GetLocation(0),
                        points[0].GetLocation(1),
                        points[0].GetLocation(2)
                        );
                last.SetWidth(SizeLines);
                last.SetColor(ColorLines);
                viewer.AddObject3D(last);
            }
            Drawing.Polygon polygon = new Drawing.Polygon(x, y);
            polygon.SetColor(Color);
            viewer.AddObject3D(polygon);
        }

    }
}
