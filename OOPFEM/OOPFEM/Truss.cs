using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Truss3DElement
    {
        private double E { get; set; }
        private double A { get; set; }
        private Node[] nodes;
        public int[] TArray { get; set; }

        public Truss3DElement(double E, double A, Node node1, Node node2)
        {
            this.E = E;
            this.A = A;
            this.nodes = new Node[] { node1, node2 };
        }
        public double GetLength()
        {
            return nodes[0].GetDistance(nodes[1]);
        }
        public Node GetNode(int index)
        {
            return nodes[index];
        }
        public int CountNodes()
        {
            return nodes.Length;
        }

        internal DenseMatrix ComputeStiffnessMatrix()
        {
            DenseMatrix Ke = new DenseMatrix(TArray.Length);
            Node n1 = nodes[0];
            Node n2 = nodes[1];
            double Cy = (n2.GetLocation(1) - n1.GetLocation(1)) / GetLength();
            double Cz = (n2.GetLocation(2) - n1.GetLocation(2)) / GetLength();
            double Cx = (n2.GetLocation(0) - n1.GetLocation(0)) / GetLength();
            double EAL = E * A * GetLength();

            Ke[0, 0] = EAL * Cx * Cx;
            Ke[0, 1] = Ke[1, 0] = EAL * Cx * Cy;
            Ke[0, 2] = Ke[2, 0] = EAL * Cx * Cz;
            Ke[0, 3] = Ke[3, 0] = -EAL * Cx * Cx;
            Ke[0, 4] = Ke[4, 0] = -EAL * Cx * Cy;
            Ke[0, 5] = Ke[5, 0] = -EAL * Cx * Cz;

            Ke[1, 1] = EAL * Cy * Cy;
            Ke[1, 2] = Ke[2, 1] = EAL * Cy * Cz;
            Ke[1, 3] = Ke[3, 1] = -EAL * Cx * Cy;
            Ke[1, 4] = Ke[4, 1] = -EAL * Cy * Cy;
            Ke[1, 5] = Ke[5, 1] = -EAL * Cy * Cz;

            Ke[2, 2] = EAL * Cz * Cz;
            Ke[2, 3] = Ke[3, 2] = -EAL * Cx * Cz;
            Ke[2, 4] = Ke[4, 2] = -EAL * Cy * Cz;
            Ke[2, 5] = Ke[5, 2] = -EAL * Cz * Cz;

            Ke[3, 3] = EAL * Cx * Cx;
            Ke[3, 4] = Ke[4, 3] = EAL * Cx * Cy;
            Ke[3, 5] = Ke[5, 3] = EAL * Cx * Cz;

            Ke[4, 4] = EAL * Cy * Cy;
            Ke[4, 5] = Ke[5, 4] = EAL * Cy * Cz;

            Ke[5, 5] = EAL * Cz * Cz;

            return Ke;
        }
    }
}
