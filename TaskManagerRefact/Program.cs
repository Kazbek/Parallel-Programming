using System;
using System.Threading;
using TaskManagerRefact.Core;

namespace TaskManagerRefact
{
    class Program
    {
        static readonly Random _rnd = new Random();
        static void Main(string[] args)
        {
            var threadManager = new TaskManager(3);
            threadManager.TaskManagerWorkDone += WorkDone;
            for (var i = 0; i < 10; i++)
            {
                var number = i;
                threadManager.AddTask(() => Print(number));
            }
            Console.WriteLine("Ожидание завершения");
            Console.ReadKey();
        }

        private static void Print(int i)
        {
            //            Console.WriteLine(string.Format("Task No {0}", i));
            Console.WriteLine($"Task No {i}");
            var workTime =_rnd.Next(1500, 5500);
            Thread.Sleep(workTime);
            Console.WriteLine($"Task No{i} finished, after {workTime}ms of work");
        }

        public static void WorkDone()
        {
            Console.WriteLine("Менеджер завершил все задачи.");
        }
    }
}
