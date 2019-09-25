using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TaskManagerSimple.Core
{
    public class ThreadManager
    {
        private readonly Queue<Action> _tasks = new Queue<Action>();
        private readonly List<ComputeThreadBasic> _threads = new List<ComputeThreadBasic>();
        private readonly Thread _mainThread;
        private bool _stop;

        public ThreadManager(int threadCount)
        {
            for (var i = 0; i < threadCount; i++)
            {
                _threads.Add(new ComputeThreadBasic(ComputeThreadBasic.GetId()));
            }
            _mainThread = new Thread(ThreadLoop);
            //            Action a = () => ThreadLoop();
            //            a.BeginInvoke()
        }

        public void Start()
        {
            _stop = false;
            _mainThread.Start();
        }

        public void Stop()
        {
            _stop = true;
        }
        public void AddTask(Action task)
        {
            //            var workers = _threads.Where(worker => worker.IsFree);
            //            if (workers.Any())
            //                workers.First().Do(task);
            //            else
            _tasks.Enqueue(task);
        }

        private void ThreadLoop()
        {
            while (!_stop)
            {
                Thread.Sleep(1);
                var workers = _threads.Where(worker => worker.IsFree).ToList();
                if (_tasks.Count > 0 && workers.Any())
                {
                    var task = _tasks.Dequeue();
                    workers.First().Do(task);
                }
            }
        }

    }
}
