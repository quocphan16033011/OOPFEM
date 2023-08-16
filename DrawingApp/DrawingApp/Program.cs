using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DEMSoft.Drawing;
using System.Drawing;
namespace DrawingApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Point p1 = new Point(new double[] { 0, 0, 0 });
            p1.Color = Color.OrangeRed;
            p1.Size = 10;
            Point p2 = new Point(new double[] { 1, 1, 1 });
            Point p3 = new Point(new double[] { 1, 2, 1 });
            Point p4 = new Point(new double[] { 2, 3, 1 });

            p2.Color = Color.OliveDrab;
            p2.Size = 20;
            Console.WriteLine($"Khoang cach giua 2 diem {p1.GetDistance(p2)}");
            ViewerForm viewer = new ViewerForm(true);
            //p2.Draw(viewer);
            //p1.Draw(viewer);
            Line line1 = new Line(1, 2, 1, 3, 3, -1);
            line1.Color = Color.Blue;
            //line1.Draw(viewer);
            PolyLine Line = new PolyLine(p1, p2, p3, p4);
            Point p10 = new Point(new double[] { 0, 0, 0 });
            Point p11 = new Point(new double[] { 3, 0, 0 });
            Point p12 = new Point(new double[] { 3, 2, 0 });
            Point p13 = new Point(new double[] { 2, 3, 0 });
            Point p14 = new Point(new double[] { 0, 1, 0 });
            List<Shape> ListShapes = new List<Shape>();

            Polygon polygon = new Polygon(p10, p11, p12, p13, p14);
            
            Triangle triangle = new Triangle(p10.GetLocation(0), p10.GetLocation(1), p10.GetLocation(2), p11.GetLocation(0), p11.GetLocation(1), p11.GetLocation(2), p12.GetLocation(0), p12.GetLocation(1), p12.GetLocation(2));

            Quadrilateral quadrature = new Quadrilateral(p10.GetLocation(0), p10.GetLocation(1), p10.GetLocation(2),p11.GetLocation(0), p11.GetLocation(1), p11.GetLocation(2), p12.GetLocation(0), p12.GetLocation(1), p12.GetLocation(2), p13.GetLocation(0), p13.GetLocation(1), p13.GetLocation(2));

            Circle circle = new Circle(new Point(new double[] { 1, 1, 0 }), 1);
            ListShapes.Add(polygon);
            ListShapes.Add(triangle);
            ListShapes.Add(quadrature);
            ListShapes.Add(circle);
            //Point pp = new Point(new double[] {1,-1,0});
            //if (polygon.IsIn(pp))
            //{
            //    pp.Color = Color.Yellow;

            //}
            //else pp.Color = Color.Red;
            //pp.Draw(viewer);
            int n = 100;
            Point[] ps = new Point[n];
            Random random = new Random();
            for (int i = 0; i < ListShapes.Count; i++)
            {
                //listpolygons[i].Draw(viewer);
            }


            for (int i = 0; i < n; i++)
            {

                double xx = getRandomNumber(random,-1,5);
                double yy = getRandomNumber(random,-1,5);
                double[] loc = { xx, yy, 0 };
                ps[i] = new Point(loc);
                ps[i].Size = 9;
                if (circle.IsIn(ps[i]))
                {
                    ps[i].Color = Color.Yellow;

                }
                else ps[i].Color = Color.Blue;
                ps[i].Draw(viewer);
            }
            //polygon.Draw(viewer);
            Console.WriteLine($"Dien tich : {polygon.GetArea()}");
            //Line.Draw(viewer);
            viewer.UpdateCamera();
            viewer.Run();
            Console.ReadKey();
        }
        public static double getRandomNumber(Random random,double a, double b)
        {
            //Random random = new Random();
            double ran = random.NextDouble();
            return (b - a) * ran + a;
        }
    }
}
