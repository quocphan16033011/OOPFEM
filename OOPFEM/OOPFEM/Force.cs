using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Force : AbstractLoad
    {
        private Node node;

        public Force(Node node, double fx, double fy, double fz)
        {
            this.node = node;
            valueForce = new double[] { fx, fy, fz };
        }
        public Node GetNode()
        {
            return node;
        }

        internal override DenseVector ComputeVectorFroce()
        {
            return new DenseVector(valueForce);
        }
    }
}
