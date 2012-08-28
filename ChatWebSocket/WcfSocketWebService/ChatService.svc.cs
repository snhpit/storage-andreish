using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Fleck;

namespace WcfSocketWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ChatService : IChatService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void StartServer()
        {
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://localhost:8181");
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

            //server.Close();

            //ServiceHost host = new ServiceHost(typeof(MyService));
        }
    }
}
