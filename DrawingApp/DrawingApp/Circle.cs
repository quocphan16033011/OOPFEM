using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Drawing = DEMSoft.Drawing;

namespace DrawingApp
{
    class Circle : Shape
    {
        private Point point;
        private double radius;
        public Circle(Point point, double radius)
        {
            this.point = point;
            this.radius = radius;
            color = Color.Peru;
        }
        public override bool IsIn(Point pointorder)
        {

            return (Math.Pow(pointorder.GetLocation(0)-point.GetLocation(0),2)+ Math.Pow(pointorder.GetLocation(1) - point.GetLocation(1), 2) - radius*radius) <=0 ;
        }
        public override double GetArea()
        {
            return Math.PI * radius * radius;
        }
        public override void Draw(Drawing.ViewerForm viewer)
        {

            point.Draw(viewer);
            point.Color= Color.Red;
            Drawing.Circle circle = new Drawing.Circle(radius, point.GetLocation(0), point.GetLocation(1));
            circle.SetColor(color);
            viewer.AddObject3D(circle);
        }

    }
}
