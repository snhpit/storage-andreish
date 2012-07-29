using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Fleck;

namespace Chat
{
    internal class Program
    {
        private static readonly string Path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

        private static void Main(string[] args)
        {
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://localhost:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    var date = DateTime.Now.ToShortTimeString();
                    //SaveMessage(socket.ConnectionInfo.Id, message);
                    Console.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Send(date + ": " + message));
                };
            });

            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }

            Console.ReadKey(true);
        }

        private static void SaveMessage(Guid id, string message)
        {
            var date = DateTime.Now.ToShortTimeString();
            File.AppendAllText(Path + "chat.txt", id + ": " + date + ": " + message + Environment.NewLine);
        }
    }
}