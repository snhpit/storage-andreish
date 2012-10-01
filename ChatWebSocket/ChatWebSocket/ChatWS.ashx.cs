using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace ChatWebSocket
{
    /// <summary>
    /// Summary description for ChatWS
    /// </summary>
    public class ChatWs : IHttpAsyncHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.R  
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            throw new NotImplementedException();
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}