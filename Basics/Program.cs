using System;
using System.Threading;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Thread th1 = new Thread(Counter);
            Thread th2 = new Thread(Counter);
            Thread th3 = new Thread(Counter);
            th1.Start();
            th2.Start();
            th3.Start();
            Console.ReadKey();
        }

        public static void Counter() => Counter(10);

        public static void Counter(int to)
        {
            for (int i = 0; i < to; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} досчитал до: {i}");
                Thread.Sleep(new Random().Next(100, 10000));
            }
        }
    }
}
