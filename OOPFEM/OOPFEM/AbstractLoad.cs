using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    internal abstract class AbstractLoad
    {
        protected double[] valueForce;
        public double GetValueForce(int direction)
        {
            return valueForce[direction];
        }

        internal abstract DenseVector ComputeVectorFroce();
    }
}
