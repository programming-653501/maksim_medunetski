using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_1
{
    class Polynom
    {
        private List<double> a;
        
        public Polynom(params double[] _a)
        {
            a = new List<double>();
            a.AddRange(_a);
        }

        public void Write(int n = 0)
        {
            if (n > 0)
                Console.Write($"{n} - ");
            foreach (double el in a)
            {
                Console.Write($"{el} ");
            }
            Console.WriteLine();
        }

        private static void swap(ref Polynom a, ref Polynom b)
        {
            Polynom t = a;
            a = b;
            b = t;
        }

        private static Polynom sum(Polynom A, Polynom B)
        {
            if (A.a.Count < B.a.Count)
                swap(ref A, ref B);
            double[] a = new double[A.a.Count];
            var ae = A.a.GetEnumerator();
            var be = B.a.GetEnumerator();
            int al = A.a.Count;
            int bl = B.a.Count;
            for (int i = 0; i < bl; i++)
            {
                ae.MoveNext();
                be.MoveNext();
                a[i] = ae.Current + be.Current;
            }
            for (int i = bl; i < al; i++)
            {
                ae.MoveNext();
                a[i] = ae.Current;
            }
            return new Polynom(a);
        }

        private static Polynom mul(Polynom A,Polynom B)
        {
            if (A.a.Count < B.a.Count)
                swap(ref A, ref B);
            var ae = A.a.GetEnumerator();
            int al = A.a.Count;
            int bl = B.a.Count;
            double[] a = new double[al + bl];
            for (int i = 0; i < al; i++)
            {
                ae.MoveNext();
                var be = B.a.GetEnumerator();
                double curA = ae.Current;
                for (int j = 0; j < bl; j++)
                {
                    be.MoveNext();
                    double curB = be.Current;
                    a[i + j] += curA * curB;
                }
            }
            return new Polynom(a);
        }

        public static Polynom operator +(Polynom A,Polynom B)
        {
            return sum(A, B);
        }

        public static Polynom operator -(Polynom A,Polynom B)
        {
            for (int i = 0; i < B.a.Count; i++)
                B.a[i] *= -1;
            Polynom ret = sum(A, B);
            for (int i = 0; i < B.a.Count; i++)
                B.a[i] *= -1;
            return ret;
        }

        public static Polynom operator *(Polynom A, Polynom B)
        {
            return mul(A, B);
        }

        public static Polynom operator /(Polynom A, Polynom B)
        {

            for (int i = 0; i < B.a.Count; i++)
                B.a[i] = 1 / B.a[i];
            Polynom ret = mul(A, B);
            for (int i = 0; i < B.a.Count; i++)
                B.a[i] = 1 / B.a[i];
            return ret;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Polynom> list = new List<Polynom>();
            while (true)
            {
                Console.WriteLine("1-enter polynom\n2-show polynoms\n3-sum polynoms\n4-deduct polynoms\n5-multiply polynoms\n6-divide polynoms\n7-about\n8-exit");
                try
                {
                    int v = Convert.ToInt32(Console.ReadLine());
                    switch(v)
                    {
                        case 1:
                            Console.WriteLine("Write polynom (format: [a1 a2 a3])");
                            string s = Console.ReadLine();
                            string[] sa = s.Split();
                            double[] a = new double[sa.Length];
                            for (int i = 0; i < sa.Length; i++)
                                a[i] = Convert.ToDouble(sa[i]);
                            a.Reverse();
                            list.Add(new Polynom(a));
                            break;
                        case 2:
                            int isw = 1;
                            foreach (Polynom p in list)
                            {
                                p.Write(isw);
                                isw++;
                            }
                            break;
                        case 3:
                            Console.WriteLine("Polynom numbers [p1 p2]");
                            sa = Console.ReadLine().Trim().Split();
                            int A = Convert.ToInt32(sa[0]);
                            int B = Convert.ToInt32(sa[1]);
                            Polynom ans = list[A-1] + list[B-1];
                            ans.Write();
                            break;
                        case 4:
                            Console.WriteLine("Polynom numbers [p1 p2]");
                            sa = Console.ReadLine().Split();
                            A = Convert.ToInt32(sa[0]);
                            B = Convert.ToInt32(sa[1]);
                            (list[A-1] - list[B-1]).Write();
                            break;
                        case 5:
                            Console.WriteLine("Polynom numbers [p1 p2]");
                            sa = Console.ReadLine().Split();
                            A = Convert.ToInt32(sa[0]);
                            B = Convert.ToInt32(sa[1]);
                            (list[A-1] * list[B-1]).Write();
                            break;
                        case 6:
                            Console.WriteLine("Polynom numbers [p1 p2]");
                            sa = Console.ReadLine().Split();
                            A = Convert.ToInt32(sa[0]);
                            B = Convert.ToInt32(sa[1]);
                            (list[A-1] / list[B-1]).Write();
                            break;
                        case 7:
                            Console.WriteLine("Lab 2\nTask 1\nAuthor: Maxim Medunetsky");
                            break;
                        case 8:
                            return;
                        default:
                            Console.WriteLine("Try again");
                            break;       
                    }
                }
                catch
                {
                    Console.WriteLine("Something is wrong! Try again");
                }
            }
        }
    }
}
