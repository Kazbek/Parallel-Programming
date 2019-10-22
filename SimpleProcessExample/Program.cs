using System;
using System.Diagnostics;
using System.Threading;

namespace SimpleProcessExample
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length == 0)
                return MainProcess();
            else
                return SubProcess(args);
        }

        private static Process savedProcess;
        static int MainProcess()
        {
            Console.WriteLine("This is Main Process!");
            Console.WriteLine($"File: {Process.GetCurrentProcess().MainModule.ModuleName}");
            Process process = new Process();
            savedProcess = process;
            process.StartInfo.FileName = Process.GetCurrentProcess().MainModule.ModuleName;
            process.StartInfo.Arguments = "first second third 1 2 3";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = true;
            
            process.EnableRaisingEvents = true;
            process.Exited += ProcessOnExited;

            //process.StartInfo.RedirectStandardOutput = true;
            //process.OutputDataReceived += ProcessOnOutputDataReceived;

            process.Start();
            Console.ReadKey();
            return 0;
        }

        private static void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"Process {((Process)sender).Id} send: {e.Data}");
        }

        private static void ProcessOnExited(object sender, EventArgs e)
        {
            Console.WriteLine($"Exited type:{sender.GetType().FullName}");
            Console.WriteLine($"Same process:{sender == savedProcess}");
            Console.WriteLine($"Same process:{savedProcess.ExitCode}");
        }

        static int SubProcess(string[] args)
        {
            Console.WriteLine("This is SUB Process!");
            foreach (string arg in args)
            {
                Console.WriteLine($"Arg: {arg}");
            }
            Thread.Sleep(2000);
            return 43534536;
        }


    }
}
