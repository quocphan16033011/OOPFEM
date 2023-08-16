using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEMSoft.Plot;
using System.Drawing;

namespace OOPFEM
{
    internal class Program
    {
        enum orentations
        {
            north,
            south,
            west,
            east
        }
        static int MaxValue(out int maxIndex, params int[] intArray)
        {
            int maxVal = intArray[0];
            maxIndex = 0;
            for (int i = 1; i < intArray.Length; i++)
            {
                if (intArray[i] > maxVal)
                {
                    maxVal = intArray[i];
                    maxIndex = i;
                }
            }
            return maxVal;
        }
        static int SumVals(params int[] vals)
        {
            int sum = 0;
            foreach (int val in vals)
            {
                sum += val;
            }
            return sum;
        }
        static void ShowDouble(ref int val)
        {
            val *= 2;
            Console.WriteLine($"val doubled = {val}");
        }
        static void Main()
        {
            //int[] myArray = { 1, 5, 2, 9, 8 };
            //int maxIndex;
            //Console.WriteLine($"The maximum value in myArray is {MaxValue(out maxIndex,myArray)}");
            //Console.WriteLine($"The first occurrence of this value is at element {maxIndex+1}");

            //int myNumber = 5;
            //Console.WriteLine($"myNumber = {myNumber}");
            //ShowDouble(ref myNumber);
            //Console.WriteLine($"myNumber = {myNumber}");
            //Rectangle rect1 = new Rectangle(0, 0, 3, 2);
            //Console.WriteLine(rect1.GetArea());
            //double[] res = rect1.GetCentriod();
            //foreach(double i in res)
            //{
            //    Console.Write($"{i} ");
            //}
            Dathuc_Info dathuc = new Dathuc_Info(3, 4, 1);
            Console.WriteLine(dathuc.InHamSo());
            Dathuc_Info dathuc2 = new Dathuc_Info(2, -5);
            Console.WriteLine(dathuc2.InHamSo());
            sin sin1 = new sin(2, 3 , dathuc2);
            Console.WriteLine(sin1.InHamSo());
            Console.WriteLine(sin1.TinhDaThuc(1));
            HamSoCong cong1 = new HamSoCong(2, dathuc, 3, sin1);
            Console.WriteLine(cong1.InHamSo());
            Plotter plotter = new Plotter();
            dathuc.Plot(plotter, -7, 6, 50, Color.Red);
            sin1.Plot(plotter, -7, 6, 50, Color.Blue);
            cong1.Plot(plotter, -7, 6, 50, Color.Violet);
            plotter.Plot();
            Dathuc_Info dathuccong = dathuc.Nhan(dathuc2);
            Console.WriteLine(dathuccong.InHamSo());
            //Console.ReadKey();
        }
        //static void Main()
        //{
        //    //orentations huong = orentations.west;
        //    //Console.WriteLine(huong);
        //    //double[,] hillHeight = { { 1, 2, 3, 4 }, { 2, 3, 4, 5 }, { 3, 4, 5, 6 } };
        //    //foreach (double height in hillHeight)
        //    //{
        //    //    Console.WriteLine("{0}", height);
        //    //}
        //    int[][] jaggedIntArray = new int[3][] {
        //        new int[] {1,2,3}, new int[] {1}, new int[] {1,2}
        //    };

        //    for (int i = 0; i < jaggedIntArray.Length; i++)
        //    {
        //        foreach (int val in jaggedIntArray[i])
        //        {
        //            Console.Write("{0} ", val);
        //        };
        //        Console.Write("\n");
        //    }
        //    Console.Write("\n");

        //    string myString = "This is a test .";
        //    char[] separator = { ' ' };
        //    string[] myWords;
        //    myWords = myString.Split(separator);
        //    for (int i = myWords.Length - 1; i >= 0; i--)
        //    {
        //        Console.Write($"{myWords[i]} ");
        //    }
        //    Console.ReadKey();
        //}
        //static void Main()
        //{
        //    int n = 4;
        //    int giaithua = 1;
        //    int i = 1;
        //    while (true)
        //    {
        //        giaithua *= i++;
        //        if(i>n) break;
        //    }
        //    Console.WriteLine($"Gia tri: {n}! = {Giaithua(n)}");
        //    Console.ReadKey();
        //}
        //public static int Giaithua(int n)
        //{
        //    if (n == 0) return 1;
        //    return Giaithua(n - 1) * n;
        //} 
        //static  void Main()
        //{
        //    Console.Write("Nhap so a : ");
        //    int a = int.Parse(Console.ReadLine());
        //    exercise2.InfinityCheck(a);
        //    exercise2.Fibonacci(a);
        //    Console.WriteLine("Nhap so n : ");

        //    int n = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Nhap so k : ");
        //    int k = int.Parse(Console.ReadLine());
        //    exercise2.Tohopchinhhop(n, k);
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hinh chu nhat dac");
        //    Console.Write("Nhap so a :");
        //    int a = int.Parse(Console.ReadLine());
        //    Console.Write("Nhap so b :");
        //    int b = int.Parse(Console.ReadLine());
        //    for (int i =0; i < b; i++)
        //    {
        //        for (int j=0;j< a; j++)
        //        {
        //            Console.Write("*");
        //        }
        //        Console.Write("\n");
        //    }
        //    Console.WriteLine("Hinh chu nhat rong");
        //    Console.Write("Nhap so a :");
        //    int a1 = int.Parse(Console.ReadLine());
        //    Console.Write("Nhap so b :");
        //    int b1 = int.Parse(Console.ReadLine());
        //    for (int i =0;i< b1; i++)
        //    {
        //        if (i == 0 || i == b1-1)
        //        {
        //            for (int j = 0; j < a1; j++)
        //            {
        //            Console.Write("*");
        //            }
        //        } else
        //        {
        //            for (int z=0;z< a1; z++)
        //            {
        //                if (z == 0 || z == a1 - 1)
        //                {
        //                    Console.Write("*");
        //                }
        //                else Console.Write(" ");
        //            }
        //        }
        //        Console.Write("\n");
        //    }
        //    Console.ReadKey();
        //}
        //public static void Main()
        //{
        //    Console.WriteLine("Nhap toa do x cua hinh vuong");
        //    double x0 = double.Parse(Console.ReadLine());
        //    Console.WriteLine("Nhap toa do y cua hinh vuong");
        //    double y0 = double.Parse(Console.ReadLine());
        //    Console.WriteLine("Nhap canh cua hinh vuong");
        //    double a = double.Parse(Console.ReadLine());
        //    InfoHinhVuong(x0, y0, a);
        //}

        //private static void InfoHinhVuong(double x0, double y0, double a)
        //{
        //    Chuvihinhvuong(a);
        //    Hinhvuong(x0, y0, a);
        //    Console.ReadKey();
        //}

        //public static double Chuvihinhvuong(double a)
        //{
        //    double cv = a * 4;
        //    Console.WriteLine($"Chu vi hinh vuong co canh la {a} : {cv}");
        //    return cv;
        //}
        //public static double[,] Hinhvuong(double x, double y, double a)
        //{
        //    double x_tam = x + a / 2;
        //    double y_tam = y + a / 2;
        //    double[,] tam = new double[1, 2];
        //    tam[0, 0] = x_tam;
        //    tam[0, 1] = y_tam;
        //    Console.WriteLine($"Tam hinh vuong co toa do ({x},{y}) co canh laf {a} : ({tam[0, 0]},{tam[0, 1]})");
        //    return tam;
        //}
        //public static double[,] Hinhchunhat(double x, double y, double a , double h)
        //{
        //    double[,] tam = new double[1, 1];
        //    double x_tam = x + a / 2;
        //    double y_tam = y + h / 2;
        //    tam[0, 0] = x_tam;
        //    tam[0, 1] = y_tam;
        //    return tam;
        //}
        //public static double[,] Hinhtamgiac(double x_1, double y_1,double x_2,double y_2, double x_3, double y_3)
        //{
        //    double[,] tam = new double[1, 1];
        //    double x_tam = (x_1 + x_2 + x_3) / 3;
        //    double y_tam = (y_1 + y_2 + y_3) / 3;
        //    tam[0, 0] = x_tam;
        //    tam[0, 1] = y_tam;
        //    return tam;
        //}

    }
    class Rectangle
    {
        double x0;
        double y0;
        double w;
        double h;
        public Rectangle(double x0, double y0, double w, double h)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.w = w;
            this.h = h;
        }
        public double GetArea()
        {
            return w * h;
        }
        public double[] GetCentriod()
        {
            return new double[] { x0 + w / 2, y0 + h / 2 };
        }
    }
}
