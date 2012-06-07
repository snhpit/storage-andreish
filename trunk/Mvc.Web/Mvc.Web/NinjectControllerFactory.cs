using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace Mvc.Web
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

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
            //_ninjectKernel.Bind<IProvider>().To<GoogleProvider>();
            //_ninjectKernel.Bind<IProvider>().To<YahooProvider>();
        }
    }
}