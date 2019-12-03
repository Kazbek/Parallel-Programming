using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipAddress = "127.0.0.1";
            int port = 11111;
            if (args.Length == 0)
            {
                Thread thread = new Thread(StartCients);
                thread.Start();
                ServerLogic.Run(ipAddress, port);
            }
            else
            {
                Console.WriteLine("Клиент инициализирован!");
                Thread.Sleep(500);
                Console.WriteLine("Начало логической части клиента!");
                ClientLogic.Run(ipAddress, port);
            }

        }

        static void StartCients()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Начало запуска дополнительных экзмпляров!");
            for (int i = 0; i < 4; i++)
            {
                Process process = new Process();
                process.StartInfo.FileName = Process.GetCurrentProcess().MainModule.ModuleName;
                process.StartInfo.Arguments = "first second third 1 2 3";
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = true;

                process.EnableRaisingEvents = true;


                //process.StartInfo.RedirectStandardOutput = true;
                //process.OutputDataReceived += ProcessOnOutputDataReceived;

                process.Start();
            }
        }
    }
}
