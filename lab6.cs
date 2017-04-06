using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Node
    {
        Node l, r;
        int children;
        public int Value { get; private set; }

        public Node(int val)
        {
            children = 0;
            Value = val;
        }

        public void Add(int val)
        {
            children++;
            if (val < Value)
            {
                if (l == null)
                    l = new Node(val);
                else
                    l.Add(val);
            }
            else
            {
                if (r == null)
                    r = new Node(val);
                else
                    r.Add(val);
            }
        }

        public int Cut()
        {
            int x;
            if ((l == null) && (r == null))
                return 0;
            if (l == null) 
            {
                x = r.Cut();
                children -= x;
                return x;
            }
            if (r == null)
            {
                x = l.Cut();
                children -= x;
                return x;
            }
            if ((l.children == 0) && (r.children == 0))
            {
                children--;
                r = null;
                return 1;
            }
            x = l.Cut() + r.Cut();
            return x;
        }

        public Node Find(int val)
        {
            if (val == Value)
                return this;
            if (val < Value)
            {
                if (l != null)
                    return l.Find(val);
                return null;
            }
            else
            {
                if (r != null)
                    return r.Find(val);
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int x = -1;
            Queue<int> q = new Queue<int>();
            do
            {
                x = int.Parse(Console.ReadLine());
                q.Enqueue(x);
            } while (x != -1);
            Node tree = new Node(q.Dequeue());
            while (q.Count > 0)
                tree.Add(q.Dequeue());
            tree.Cut();
            while (true)
            {
                x = int.Parse(Console.ReadLine());
                if (tree.Find(x) == null)
                    Console.WriteLine("FALSE");
                else
                    Console.WriteLine("TRUE");
            }
        }
    }
}
