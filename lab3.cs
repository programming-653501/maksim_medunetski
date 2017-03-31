using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int m = Convert.ToInt32(Console.ReadLine());
            byte[,] a = new byte[n + 1, m + 1];
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                    a[i, j] = (byte)rnd.Next(0, 2);
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                    Console.Write($"{a[i, j]} ");
                Console.WriteLine();
            }
            int size = Convert.ToInt32(Console.ReadLine());
            int[,] dp = new int[n + 1, m + 1];
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    dp[i, j] = Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1])) + 1;
                    if (a[i, j] == 1)
                        dp[i, j] = 0;
                    if (dp[i, j] == size)
                    {
                        Console.WriteLine($"{i - size + 1} {j - size + 1}");
                        Console.ReadKey();
                        return;
                    }
                }
            Console.WriteLine("Nothing");
            Console.ReadKey();
        }
    }
}
