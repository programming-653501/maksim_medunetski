using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab4_2
{
    class Program
    {
        static bool Check(string s)
        {
            string symbols = "ABCEKMHOPTX";
            foreach (char c in s)
            {
                if (symbols.IndexOf(c) == -1)
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string str = reader.ReadToEnd();
                string[] words = str.Split(new char[] { ' ', '.', ',','\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    //Console.WriteLine(word);
                    if (Check(word))
                        Console.Write(word + " ");
                }
            }
            Console.ReadKey();
        }
    }
}
