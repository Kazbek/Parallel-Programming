using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClientServer
{
    public class ServerConnectionHandler
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _listenerThread;
        public ServerConnectionHandler(TcpClient tcpClient)
        {
            _client = tcpClient;
            _stream = tcpClient.GetStream();
            _listenerThread = new Thread(GetMessages);
            _listenerThread.Start();
        }

        public void GetMessages()
        {
            while (true)
            {
                //Console.WriteLine("Виток ServerConnectionHandler-GetMessages");

                var bufferSize = _client.ReceiveBufferSize;
                byte[] instream = new byte[bufferSize];
                _stream.Read(instream, 0, bufferSize);
                string returnData = Encoding.UTF8.GetString(instream);
                returnData = returnData.Trim((char)0);
                Console.WriteLine($"Received message: \"{returnData}\"");
            }
        }

        public void SendMessage(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            _stream.Write(data, 0, data.Length);
            _stream.Flush();
        }

    }
}
