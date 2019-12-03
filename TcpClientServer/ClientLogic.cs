using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClientServer
{
    public static class ClientLogic
    {
        private static TcpClient _client;
        private static NetworkStream _serverStream = default(NetworkStream);

        private static Thread _listenerThread;
        public static void Run(string ipAddress, int port)
        {
            try
            {
                int myNum = new Random().Next(1000000);
                _client = new TcpClient();
                _client.Connect(ipAddress, port);

                byte[] data = GetMesageBytes($"Hello server I`m {myNum}!");
                StringBuilder response = new StringBuilder();
                _serverStream = _client.GetStream();
                NetworkStream stream = _client.GetStream();

                _listenerThread = new Thread(GetMessages);
                _listenerThread.Start();
                Console.WriteLine("Отправка сообщения");
                stream.Write(data, 0, data.Length);
                stream.Flush();
                Console.WriteLine("Сообщение отправлено");

                while (true)
                {
                    string msg = Console.ReadLine();
                    if(msg == null)
                        continue;
                    byte[] sendData = Encoding.UTF8.GetBytes($"{myNum}: {msg}");
                    _serverStream.Write(sendData, 0, sendData.Length);
                    _serverStream.Flush();
                }

                /*do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable); // пока данные есть в потоке
                */
                //Console.WriteLine(response.ToString());

                // Закрываем потоки
                //stream.Close();
                //_client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            Console.WriteLine("Запрос завершен...");
            Console.Read();
        }


        private static void GetMessages()
        {
            while (true)
            {
                var bufferSize = _client.ReceiveBufferSize;
                byte[] instream = new byte[bufferSize];

                _serverStream.Read(instream, 0, bufferSize);
                string returnData = Encoding.UTF8.GetString(instream);
                returnData = returnData.Trim((char)0);
                Console.WriteLine($"Received message: {returnData}");
            }
        }

        private static byte[] GetMesageBytes(string msg)
        {
            return Encoding.UTF8.GetBytes(msg);
        }
    }
}
