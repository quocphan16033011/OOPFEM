using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Q4Element : AbstractPlaneElement
    {
        public Q4Element(double E, double nu, Node node1, Node node2, Node node3, Node node4)
        {
            this.E = E;
            this.nu = nu;
            this.nodes = new Node[] { node1, node2, node3, node4 };
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
            DenseMatrix Ke = new DenseMatrix(2 * nodes.Length);
            // Plane stress
            DenseMatrix D = new DenseMatrix(3);
            double a = E / (1 - nu * nu);
            D[0, 0] = a;
            D[1, 0] = D[0, 1] = a * nu;
            D[1, 1] = a;
            D[2, 2] = a * (1 - nu) / 2.0;
            double[] xi = { -1.0 / Math.Sqrt(3), 1.0 / Math.Sqrt(3) };
            int numberOfGauss = 2;
            for (int i = 0; i < numberOfGauss; i++)
            {
                for (int j = 0; j < numberOfGauss; j++)
                {
                    double xiI = xi[i];
                    double etaI = xi[j];
                    double wi = 1;
                    double wj = 1;
                    DenseMatrix dNdxi = new DenseMatrix(2, nodes.Length);
                    dNdxi[0, 0] = -1.0 / 4.0 * (1 - etaI);
                    dNdxi[0, 1] = 1.0 / 4.0 * (1 - etaI);
                    dNdxi[0, 2] = 1.0 / 4.0 * (1 + etaI);
                    dNdxi[0, 3] = -1.0 / 4.0 * (1 + etaI);

                    dNdxi[1, 0] = -1.0 / 4.0 * (1 - xiI);
                    dNdxi[1, 1] = -1.0 / 4.0 * (1 + xiI);
                    dNdxi[1, 2] = 1.0 / 4.0 * (1 + xiI);
                    dNdxi[1, 3] = 1.0 / 4.0 * (1 - xiI);

                    DenseMatrix Jacobi = new DenseMatrix(2, 2);
                    for (int ii = 0; ii < 2; ii++)
                    {
                        for (int jj = 0; jj < 2; jj++)
                        {
                            for (int kk = 0; kk < nodes.Length; kk++)
                            {
                                Jacobi[ii, jj] += dNdxi[ii, kk] * nodes[kk].GetLocation(jj);
                            }
                        }
                    }
                    double detJ = Math.Abs(Jacobi.Determinant());
                    DenseMatrix B = new DenseMatrix(3, 2 * nodes.Length);
                    for (int kk = 0; kk < nodes.Length; kk++)
                    {
                        B[0, 2 * kk] = Jacobi[1, 1] * dNdxi[0, kk] - Jacobi[0, 1] * dNdxi[1, kk];
                        B[1, 2 * kk + 1] = -Jacobi[1, 0] * dNdxi[0, kk] - Jacobi[0, 0] * dNdxi[1, kk];
                        B[2, 2 * kk] = -Jacobi[1, 0] * dNdxi[0, kk] - Jacobi[0, 0] * dNdxi[1, kk];
                        B[2, 2 * kk + 1] = Jacobi[1, 1] * dNdxi[0, kk] - Jacobi[0, 1] * dNdxi[1, kk];

                    }
                    B = (DenseMatrix)(B / detJ);
                    Ke += (DenseMatrix)(wi * wj * detJ * B.Transpose() * D * B);
                }
            }
            return Ke;
        }
    }
}
