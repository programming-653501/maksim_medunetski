using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            StringBuilder newStr = new StringBuilder();
            int counter = 0;
            foreach (char c in input)
            {
                if (c == '(')
                {
                    counter++;
                    newStr.Append(c);
                }
                if (c == ')')
                {
                    counter--;
                    newStr.Append(c);
                }
                if (counter < 0) 
                {
                    Console.WriteLine("Incorrect bracket sequence");
                    Console.ReadKey();
                    return;
                }
                if ((counter == 0) && (c != ')')) 
                {
                    newStr.Append(c);
                }
            }
            if (counter > 0)
            {
                Console.WriteLine("Incorrect bracket sequence");
                Console.ReadKey();
                return;
            }
            Console.WriteLine(newStr);
            Console.ReadKey();
        }
    }
}
