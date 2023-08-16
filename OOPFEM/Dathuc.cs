using System;
using DEMSoft.Plot;
using System.Drawing;

namespace OOPFEM
{
    internal class Dathuc_Info : function
    {
        private double[] coef;
        private function gx;

        public Dathuc_Info(params double[] coef)
        {
            this.coef = coef;
            gx = null;
        }

        public Dathuc_Info(double[] coef, function gx)
        {
            this.coef = coef;
            this.gx = gx;
        }

        public int BacDaThuc()
        {
            return coef.Length - 1;
        }
        public double HeSo(int n)
        {
            if (n > BacDaThuc()) return 0;
            return coef[n];
        }
        public override double TinhDaThuc(double x)
        {
            double result = 0;
            for (int i = 0; i < coef.Length; i++)
            {
                result += HeSo(i) * Math.Pow(x, i);
            }
            return result;
        }
        public Dathuc_Info Cong(Dathuc_Info daThuc)
        {
            int max = Math.Max(BacDaThuc(), daThuc.BacDaThuc());

            double[] hesomoi = new double[max + 1];
            for (int i = 0; i < max + 1; i++)
            {
                hesomoi[i] = HeSo(i) + daThuc.HeSo(i);
            }

            return new Dathuc_Info(hesomoi);
        }
        public Dathuc_Info Nhan(double a)
        {
            double[] hesomoi = new double[BacDaThuc() + 1];
            for (int i = 0; i < coef.Length; i++)
            {
                hesomoi[i] = a * HeSo(i);
            }
            return new Dathuc_Info(hesomoi);
        }
        public Dathuc_Info Nhan(Dathuc_Info dathuc)
        {
            double[] HeSoMoi = new double[BacDaThuc() + dathuc.BacDaThuc() + 1];
            for (int i = 0; i < coef.Length; i++)
            {
                for (int j = 0; j < dathuc.coef.Length; j++)
                {
                    HeSoMoi[i + j] += coef[i] * dathuc.coef[j];
                }
            }
            return new Dathuc_Info(HeSoMoi);
        }
        public override string InHamSo()
        {
            string str = "";
            for (int i = 0; i < coef.Length; i++)
            {
                if (i == coef.Length - 1)
                {
                    str += coef[i] + " * x^" + i;
                }
                else
                {
                    str += coef[i] + " * x^" + i + " + ";
                }
            }
            return str; 
        }
    }
}
