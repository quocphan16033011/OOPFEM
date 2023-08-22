using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    internal class T3Elements : AbstractELement
    {
        private double nu { get; set; }

        public T3Elements(double E, double nu, Node node1, Node node2, Node node3)
        {
            this.E = E;
            this.nu = nu;
            this.nodes = new Node[] { node1, node2, node3 };
        }
        
        public double GetArea()
        {
            double x1 = nodes[0].GetLocation(0);
            double y1 = nodes[0].GetLocation(1);
            double x2 = nodes[1].GetLocation(0);
            double y2 = nodes[1].GetLocation(1);
            double x3 = nodes[2].GetLocation(0);
            double y3 = nodes[2].GetLocation(1);
            return 1.0 / 2.0 * ((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));
        }
        internal override DenseMatrix ComputeStiffnessMatrix()
        {
            DenseMatrix Ke;
            double A = GetArea();
            double x1 = nodes[0].GetLocation(0);
            double y1 = nodes[0].GetLocation(1);
            double x2 = nodes[1].GetLocation(0);
            double y2 = nodes[1].GetLocation(1);
            double x3 = nodes[2].GetLocation(0);
            double y3 = nodes[2].GetLocation(1);
            // Plane stress
            DenseMatrix D = new DenseMatrix(3);
            double a = E / (1 - nu * nu);
            D[0, 0] = a;
            D[1, 0] = D[0, 1] = a * nu;
            D[1, 1] = a;
            D[2, 2] = a * (1 - nu) / 2.0;
            DenseMatrix B = new DenseMatrix(3, 6);
            B[0, 0] = B[2, 1] = (y2 - y3) / (2 * A);
            B[0, 2] = B[2, 3] = (y3 - y1) / (2 * A);
            B[0, 4] = B[2, 5] = (y1 - y2) / (2 * A);
            B[1, 1] = B[2, 0] = (x3 - x2) / (2 * A);
            B[1, 3] = B[2, 2] = (x1 - x3) / (2 * A);
            B[1, 5] = B[2, 4] = (x2 - x1) / (2 * A);
            Ke = (DenseMatrix)(A * B.Transpose() * D * B);
            return Ke;
        }
    }
}
