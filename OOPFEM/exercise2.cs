using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class exercise2
    {
        public static double InfinityCheck(int a)
        {
            Console.WriteLine("Check infinity");
            double result = 0;
            for (int i = 1; i <= a; i++)
            {
                double c = 1;
                result += c / (i * i);
            }
            if (double.IsInfinity(result))
            {
                Console.WriteLine(result);
                result = (Math.PI) * (Math.PI) / 6;
            }
            Console.WriteLine($"{result,2:E}");
            Console.ReadKey();
            return result;

        }
        public static float[] Fibonacci(int a)
        {
            Console.WriteLine("Fibonacci");
            float[] result = new float[a];
            for (int i = 0; i < a; i++)
            {
                if (i == 0 || i == 1)
                {
                    result[i] = 1;
                }
                else
                {
                    result[i] = result[i - 1] + result[i - 2];
                }
                Console.WriteLine(result[i]);
            }
            Console.ReadKey();
            return result;
        }
        public static float Tohopchinhhop(int n, int k)
        {
            float result = 1;
            Console.WriteLine("Chinh hop chap k cua n");
            if (k > n) Console.WriteLine("Khong hop le");
            else if (k == 0) result = 1;
            else if (k == n)
            {
                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }
            }
            else
            {
                float tu = 1;
                float mau = 1;
                for (int i = 1; i <= n; i++)
                {
                    tu *= i;
                }
                for (int i = 1; i <= n - k; i++)
                {
                    mau *= i;
                }
                result = tu / mau;
            }
            Console.WriteLine(result);
            result = 0;
            Console.WriteLine("To hop chap k cua n");
            if (k > n)
            {
                Console.WriteLine("k khong hơp le");
            }
            else if (k == 0 || k == n)
            {
                result = 1;
            }
            else
            {
                float tu = 1;
                float mau1 = 1;
                float mau2 = 1;
                for (int i = 1; i <= n; i++)
                {
                    tu *= i;
                }
                for (int i = 1; i <= n - k; i++)
                {
                    mau2 *= i;
                }
                for (int i = 1; i <= k; i++)
                {
                    mau1 *= i;
                }
                result = tu / (mau1 * mau2);
            }
            Console.WriteLine(result);
            Console.ReadKey();
            return result;
        }
    }
}
