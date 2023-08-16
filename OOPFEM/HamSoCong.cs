using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    internal class HamSoCong : function
    {
        private double a;
        private double b;
        private function g1;
        private function g2;
        public  HamSoCong(double a, function g1, double b, function g2)
        {
            this.a = a;
            this.b = b;
            this.g1 = g1;
            this.g2 = g2;
        }
        public override string InHamSo()
        {
            string str = $"{a} * ( {g1.InHamSo()} ) + {b} * {g2.InHamSo()}";
            return str;
        }

        public override double TinhDaThuc(double x)
        {
            return a * g1.TinhDaThuc(x) + b * g2.TinhDaThuc(x);
        }
    }
}
