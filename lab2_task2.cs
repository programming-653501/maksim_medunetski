using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("x = ");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("eps = ");
            double eps = Convert.ToDouble(Console.ReadLine());
            double sin = Math.Sin(x);
            double[] xn = new double[100];
            double[] fac = new double[100];
            fac[0] = 1;
            xn[0] = 1;
            for (int i=1;i<100;i++)
            {
                xn[i] = xn[i - 1] * x;
                fac[i] = fac[i - 1] * i;
            }
            double ans = 0;
            int n = 1;
            do
            {
                double v = xn[2 * n - 1] / fac[2 * n - 1];
                if ((n - 1) % 2 == 0)
                    ans += v;
                else
                    ans -= v;
                n++;
            } while (Math.Abs(sin - ans) > eps);
            Console.WriteLine($"sin = {sin}, pol = {ans}, N = {n}");
            Console.ReadKey();
        }
    }
}
