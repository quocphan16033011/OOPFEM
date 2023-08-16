using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEMSoft.Plot;

namespace OOPFEM
{
    class sin : function
    {
        private double a;
        private double b;
        private function gx;
        /// <summary>
        /// Ham Sin f(x) = a * sin(b * x)
        /// </summary>

        public sin(double a, double b)
        {
            this.a = a;
            this.b = b;
            gx = null;
        }

        public sin(double a, double b , function gx)
        {
            this.a = a;
            this.b = b;
            this.gx = gx;
        }

        public override string InHamSo()
        {
            string str = $"{a} * sin ({b} * ( {gx.InHamSo()} ))";
            return str;
        }

        public override double TinhDaThuc(double x)
        {
            if (gx == null)
            {
                return a * Math.Sin(b * x);
            }
            else return a * Math.Sin(b * gx.TinhDaThuc(x));
        }

    }
}
