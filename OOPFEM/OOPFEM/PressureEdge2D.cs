using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    internal class PressureEdge2D : AbstractLoad
    {
        private T3Elements element;
        private int indexEgde;
        public PressureEdge2D(T3Elements element, int indexEgde, double px, double py)
        {
            this.element = element;
            this.indexEgde = indexEgde;
            valueForce = new double[] { px, py };
        }

        internal override DenseVector ComputeVectorFroce()
        {
            DenseVector LoadVec = new DenseVector(2 * element.CountNodes());
            Node n0, n1;
            double length, totalForceX, totalForceY;
            switch (indexEgde)
            {
                case 0:
                    n0 = element.GetNode(0);
                    n1 = element.GetNode(1);
                    length = n0.GetDistance(n1);
                    totalForceX = valueForce[0] * length;
                    totalForceY = valueForce[1] * length;
                    LoadVec[0] = totalForceX / 2.0;
                    LoadVec[1] = totalForceY / 2.0;
                    LoadVec[2] = totalForceX / 2.0;
                    LoadVec[3] = totalForceY / 2.0;
                    break;
                case 1:
                    n0 = element.GetNode(1);
                    n1 = element.GetNode(2);
                    length = n0.GetDistance(n1);
                    totalForceX = valueForce[0] * length;
                    totalForceY = valueForce[1] * length;
                    LoadVec[2] = totalForceX / 2.0;
                    LoadVec[3] = totalForceY / 2.0;
                    LoadVec[4] = totalForceX / 2.0;
                    LoadVec[5] = totalForceY / 2.0;
                    break;
                case 2:
                    n0 = element.GetNode(2);
                    n1 = element.GetNode(0);
                    length = n0.GetDistance(n1);
                    totalForceX = valueForce[0] * length;
                    totalForceY = valueForce[1] * length;
                    LoadVec[4] = totalForceX / 2.0;
                    LoadVec[5] = totalForceY / 2.0;
                    LoadVec[0] = totalForceX / 2.0;
                    LoadVec[1] = totalForceY / 2.0;
                    break;
            }
            return LoadVec;
        }
    }
}
