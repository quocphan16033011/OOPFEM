using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Constraint
    {
        private Node node;
        private bool[] isFixed;

        public Constraint(Node node, bool x, bool y, bool z)
        {
            this.node = node;
            isFixed = new bool[] { x, y, z };
        }
        public Node GetNode()
        {
            return node;
        }
        public bool IsFixed(int indexDirection)
        {
            return isFixed[indexDirection];
        }
        public int GetNumberConstraint()
        {
            return isFixed.Length;
        }
    }
}
