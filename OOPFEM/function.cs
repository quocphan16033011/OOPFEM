using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEMSoft.Plot;
using System.Drawing;

namespace OOPFEM
{
    internal abstract class function
    {
        public abstract double TinhDaThuc(double x);
        public abstract string InHamSo();
        public virtual void Plot(Plotter plotter, double x0, double x1,int n, Color color)
        {
            double[] x = new double[n];
            double[] y = new double[n];
            double dx = (x1 - x0) / (n - 1);
            for (int i = 0; i < n; i++)
            {
                x[i] = x0 + i * dx;
                y[i] = TinhDaThuc(x[i]);
            }
            PlotLine line = new PlotLine();
            line.InputData(x, y, "ham so");
            line.SetColor(color);
            plotter.AddPlot(line);
        }
    }
}
