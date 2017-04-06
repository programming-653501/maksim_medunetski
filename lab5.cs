using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Program
    {
        static Random rnd;
        static Queue<DateTime> q;
        static bool stop = false;
        static TimeSpan maxTime;
        static int maxCount = 0;

        static async void StartAddToQueue()
        {
            while (true)
            {
                int time = rnd.Next(0, 1500);
                await Task.Delay(time);
                if (stop)
                    return;
                q.Enqueue(DateTime.Now);
                maxCount = Math.Max(maxCount, q.Count);
                Console.WriteLine("Add in queue. Count: {0}", q.Count);
            }
        } 

        static async void ProcessQueue()
        {
            while (true)
            {
                int time = rnd.Next(0, 1500);
                await Task.Delay(time);
                if (stop)
                    return;
                if (q.Count > 0)
                {
                    DateTime start = q.Dequeue();
                    TimeSpan WaitingTime = DateTime.Now.Subtract(start);
                    if ((maxTime == null) || (maxTime < WaitingTime))
                    {
                        maxTime = WaitingTime;
                    }
                    Console.WriteLine("Delete from queue. Count: {0}", q.Count);
                }
            }
        }

        static void Main(string[] args)
        {
            rnd = new Random(DateTime.Now.Millisecond);
            q = new Queue<DateTime>();
            int sec = int.Parse(Console.ReadLine());
            DateTime start = DateTime.Now;
            StartAddToQueue();
            ProcessQueue();
            while (DateTime.Now.Subtract(start).Seconds < sec)
            {

            }
            stop = true;
            Console.WriteLine("Max waiting time: {0}", maxTime);
            Console.WriteLine("Max count: {0}", maxCount);
            Console.ReadKey();
        }
    }
}
