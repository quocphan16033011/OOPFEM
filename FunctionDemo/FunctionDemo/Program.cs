using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEMSoft.Function;
using DEMSoft.Plot;
using System.Drawing;

namespace FunctionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SinFunctionRToR sin1 = new SinFunctionRToR(2, 3);
            CosFunctionRToR cos1 = new CosFunctionRToR(2, 3);
            PlotFunctionRToR plot1 = new PlotFunctionRToR(-6, 6, 200);
            plot1.AddLineChart(sin1, Color.Red,3 , LineType.DOT3);
            plot1.AddPointChart(cos1, Color.Green, 10, MarkerStyle.CROSS, "2*cos(3x)");
            plot1.Plot();
        }
    }
}
