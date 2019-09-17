using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Basics
{
    public static class CounterParameterized
    {
        public static void Run()
        {
            Console.WriteLine("CounterParameterized");
            CounterWrap cp = new CounterWrap { To = 13 };
            Thread th = new Thread(cp.Count);
            th.Start();
        }
    }

    public class CounterWrap
    {
        public int To { get; set; }

        public void Count()
        {
            for (int i = 1; i <= To; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} досчитал до: {i}");
                Thread.Sleep(new Random().Next(100, 1000));
            }
        }
    }
}
