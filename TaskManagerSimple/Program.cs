using System;
using System.Threading;
using TaskManagerSimple.Core;

namespace TaskManagerSimple
{
    class Program
    {
        static readonly Random _rnd = new Random();
        static void Main(string[] args)
        {
            var threadManager = new ThreadManager(3);
            for (var i = 0; i < 10; i++)
            {
                var number = i;
                threadManager.AddTask(() => Print(number));
            }
            threadManager.Start();
            Console.ReadKey();
        }

        private static void Print(int i)
        {
            //            Console.WriteLine(string.Format("Task No {0}", i));
            Console.WriteLine($"Task No {i}");
            var workTime = 1000 + _rnd.Next(-500, 1500);
            Thread.Sleep(workTime);
            Console.WriteLine($"Task No{i} finished, after {workTime}ms of work");
        }

        
    }
}
