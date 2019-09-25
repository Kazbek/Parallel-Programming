using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basics
{
    public static class LockTest
    {
        public static int CallCounter = 0;
        public static object locker = new object();
        public static void Run()
        {
            Console.WriteLine("LockTest");

            int thCount = 10;
            List<Thread> threads = new List<Thread>(thCount);
            for(int i = 0; i < thCount; i++)
                threads.Add(new Thread(CallMeMaybe));
            //Parallel.ForEach(threads, thread => thread.Start());
            foreach (var thread in threads)
            {
                thread.Start();
            }

        }

        public static void CallMeMaybe()
        {
            for(int i = 0; i < 10; i++)
                InternalCallNonLock();
        }

        public static void InternalCall()
        {
            lock (locker)
            {
                int currentNum = CallCounter;
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал эксклюзивное использование");
                Thread.Sleep(new Random().Next(10,500));
                currentNum++;
                CallCounter = currentNum;
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} увеличил число до {currentNum}");
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} закончил эксклюзивное использование");
            }
        }

        public static void InternalCallNonLock()
        {
            int currentNum = CallCounter;
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал использование");
            Thread.Sleep(new Random().Next(10, 500));
            currentNum++;
            CallCounter = currentNum;
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} увеличил число до {currentNum}");
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} закончил использование");
        }
    }
}
