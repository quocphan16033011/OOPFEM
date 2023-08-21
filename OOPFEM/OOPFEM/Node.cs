using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Node
    {
        private double[] location;
        private double[] u;
        public int[] TArray { get; set; }
        public Node(double x, double y, double z=0)
        {
            location = new double[] { x, y, z };
            u = new double[3];
        }
        public double GetLocation(int index)
        {
            return location[index];
        }
        public double GetU(int index)
        {
            return u[index];
        }
        public void SetU(int index, double value)
        {
            u[index] = value;
        }
        public double GetDistance(Node node2)
        {
            return Math.Sqrt(Math.Pow(location[0] - node2.location[0], 2) + Math.Pow(location[1] - node2.location[1], 2) + Math.Pow(location[2] - node2.location[2], 2));
        }
    }
}
