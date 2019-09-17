using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Basics
{
    public static class Counter
    {
        public static void Run()
        {
            Console.WriteLine("Counter");
            Thread th1 = new Thread(Count);
            Thread th2 = new Thread(Count);
            Thread th3 = new Thread(Count);
            th1.Start();
            th2.Start();
            th3.Start();
            Console.ReadKey();
        }

        public static void Count() => Count(10);

        public static void Count(int to)
        {
            for (int i = 1; i <= to; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} досчитал до: {i}");
                Thread.Sleep(new Random().Next(100, 10000));
            }
        }
    }
}
