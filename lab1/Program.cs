using System;
using System.Collections.Generic;

namespace lab1
{
    class Comp:Comparer<int>
    {
        public override int Compare(int a,int b)
        {
            if (a == b)
                return 0;
            else if (a < b)
                return 1;
            else
                return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Comp comp = new Comp();
            SortedDictionary<int, string> dic = new SortedDictionary<int, string>(comp);
            dic[1] = "I";
            dic[4] = "IV";
            dic[5] = "V";
            dic[9] = "IX";
            dic[10] = "X";
            dic[40] = "XL";
            dic[50] = "L";
            dic[90] = "XC";
            dic[100] = "C";
            dic[400] = "CD";
            dic[500] = "D";
            dic[900] = "CM";
            dic[1000] = "M";
            dic[4000] = "M_V_";
            dic[5000] = "_V_";
            dic[9000] = "I_X_";
            dic[10000] = "_X_";
            int a = Convert.ToInt32(Console.ReadLine());
            string ans = "";
            foreach (var x in dic)
            {
                while (x.Key <= a)
                {
                    a -= x.Key;
                    ans += x.Value;
                }
            }
            Console.WriteLine(ans);
            Console.ReadKey();
        }
    }
}
