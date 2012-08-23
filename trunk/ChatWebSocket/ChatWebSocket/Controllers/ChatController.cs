using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fleck;

namespace ChatWebSocket.Controllers
{
    public class ChatController : Controller
    {  
        public ActionResult Index()
        {
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://localhost:8183");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    //Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    //Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    var date = DateTime.Now.ToShortTimeString();
                    //SaveMessage(socket.ConnectionInfo.Id, message);
                    //Console.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Send(date + ": " + message));
                };
            });

            return View();
        }

    }
}
