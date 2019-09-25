using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskManagerSimple.Core
{
    public class ComputeThreadBasic
    {
        private static int IdCounter = 1;

        public static int GetId()
        {
            return IdCounter++;
        }

        //private int _id;
        private Thread _worker;
        private ComputeThreadState _state;
        private Action _work;
        private bool _stop = false;


        public ComputeThreadBasic(int id)
        {
            //_id = id;
            Id = id;
            _worker = new Thread(new ThreadStart(WorkingLoop));
            _state = ComputeThreadState.Idle;
            _worker.Start();
        }

        //public int Id => _id;
        public int Id { get; }

        public bool IsBusy => _state == ComputeThreadState.Working;
        public bool IsFree => _state == ComputeThreadState.Idle;
        public bool IsTerminated => _state == ComputeThreadState.Terminated;

        public void Do(Action work)
        {
            if (_state != ComputeThreadState.Idle)
                throw new InvalidOperationException("Thread is busy or unresponding");
            _work = work;
        }

        public void Stop()
        {
            _stop = true;
        }

        private void WorkingLoop()
        {
            while (!_stop)
            {
                if (_work != null && _state == ComputeThreadState.Idle)
                {
                    Thread.Sleep(1);
                    _state = ComputeThreadState.Working;
                    _work();
                    _work = null;
                    _state = ComputeThreadState.Idle;
                }
            }
        }
    }

    public enum ComputeThreadState
    {
        Idle,
        Working,
        Terminated
    }
}
