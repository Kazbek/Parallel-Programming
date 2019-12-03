using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClientServer
{
    public static class ServerLogic
    {
        static List<ServerConnectionHandler> _clients = new List<ServerConnectionHandler>();
        static TcpListener _server = null;

        private static Thread _listenerThread;
        static readonly object ClientLockObject = new object();
        public static void Run(string ipAddress, int port)
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, port);

            // запуск слушателя
            _server.Start();
            _listenerThread = new Thread(ConnectionRegistrator);
            _listenerThread.Start();

            while (true)
            {
                string msg = Console.ReadLine();
                if(msg==null)
                    continue;
                int num = 0;
                lock (ClientLockObject)
                {
                    foreach (var client in _clients)
                    {
                        client.SendMessage($"{num++} - {msg}");
                    }
                }
            }
        }

        public static void ConnectionRegistrator()
        {
            while (true)
            {
                Console.WriteLine("Ожидание подключений... ");

                // получаем входящее подключение
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Подключен клиент.");
                lock (ClientLockObject)
                {
                    ServerConnectionHandler handler = new ServerConnectionHandler(client);
                    _clients.Add(handler);
                }
                /*

                // получаем сетевой поток для чтения и записи
                NetworkStream stream = client.GetStream();

                // сообщение для отправки клиенту
                string response = "Привет мир";
                // преобразуем сообщение в массив байтов
                byte[] data = Encoding.UTF8.GetBytes(response);
                
                // отправка сообщения
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Отправлено сообщение: {0}", response);
                // закрываем поток
                stream.Close();
                // закрываем подключение
                client.Close();*/
            }
        }
    }
}
