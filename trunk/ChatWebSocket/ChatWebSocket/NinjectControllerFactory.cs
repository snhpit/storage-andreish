using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatWebSocket.ChatWcfService;
using Ninject;
using ChatWebSocket.Controllers;
using Ninject.Syntax;
using Ninject.Parameters;

namespace ChatWebSocket
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
            //base.GetControllerInstance(requestContext, controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IChatService>().To<ChatServiceClient>();
            
            //var q = DependencyResolver.Current.GetType().GetMethod("GetService")
            //    .Invoke(DependencyResolver.Current, new[] { Type.GetType("Mvc.Web.Providers.YahooProvider") });
        }
    }
}