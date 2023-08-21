using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    internal abstract class AbstractELement
    {
        protected double E { get; set; }
        protected Node[] nodes;
        public int[] TArray { get; set; }

        public Node GetNode(int index)
        {
            return nodes[index];
        }
        public int CountNodes()
        {
            return nodes.Length;
        }
        internal abstract DenseMatrix ComputeStiffnessMatrix();

        }
}
