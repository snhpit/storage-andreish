using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 4649;
            const int coonections = 100;

            var hash = new Hash();
            
            Socket listenSocket = new Socket(AddressFamily.InterNetwork,
                                             SocketType.Stream,
                                             ProtocolType.Tcp);

            IPAddress address = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
            IPEndPoint ep = new IPEndPoint(address, port);
          
            listenSocket.Bind(ep);
            listenSocket.Connect(ep);
            listenSocket.Listen(coonections);

            var wss = new WebSocket(new Uri("ws://localhost:" + port));
            wss.Connect();
            wss.Send("hi");
            var reciveMessage = wss.Recv();

            Console.WriteLine(reciveMessage);

            string name = Dns.GetHostName();
            try
            {
                IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;
                foreach (IPAddress addr in addrs)
                    Console.WriteLine("{0}/{1}", name, addr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
