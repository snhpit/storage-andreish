using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ChatWebSocket
{
    public class ChatWs : IHttpAsyncHandler
    {

        public void ProcessRequest(HttpContext context)
        {
 
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

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        {
            context.Response.Write("<p>Begin IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");
            AsyncOperation async = new AsyncOperation(cb, context, extraData);
            async.StartAsyncWork();
            return async;
        }


        public void EndProcessRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
  
}