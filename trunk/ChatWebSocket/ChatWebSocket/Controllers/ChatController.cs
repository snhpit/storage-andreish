using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatWebSocket.ChatWcfService;
using Fleck;
using System.ServiceModel;

namespace ChatWebSocket.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService _proxy;

        public ChatController(IChatService proxy)
        {
            this._proxy = proxy;
        }

        public ActionResult Index()
        {
            ViewBag.Data = _proxy.GetData(5);

            //_proxy.StartServer();

            return View();
        }

    }
}
