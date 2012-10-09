using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ChatWebSocket
{
    class AsyncOperation : IAsyncResult
    {
        private bool _completed;
        private Object _state;
        private AsyncCallback _callback;
        private HttpContext _context;

        bool IAsyncResult.IsCompleted { get { return _completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return _state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        public AsyncOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {

            _context.Response.Write("<p>Completion IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");

            _context.Response.Write("Hello World from Async Handler!");
            _completed = true;
            _callback(this);
        }
    }
}