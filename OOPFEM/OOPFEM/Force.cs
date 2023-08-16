using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Force
    {
        private double[] valueForce;
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
        public double GetValueForce(int direction)
        {
            return valueForce[direction];
        }

        internal DenseVector ComputeVectorFroce()
        {
            return new DenseVector(valueForce);
        }
    }
}
