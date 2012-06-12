using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Web.Controllers;
using Mvc.Web.Converters;
using Mvc.Web.Providers;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using Ninject.Web.Mvc;

namespace Mvc.Web
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
            //_ninjectKernel.Bind<IProvider>().To<YahooProvider>();
            var h = _ninjectKernel.Bind<IProvider>().To<GoogleProvider>();//.ToMethod<YahooProvider>(context => context.Parameters.Count == 3);

            //_ninjectKernel.Bind<IConverter>().To<XmlConverter>();
            _ninjectKernel.Bind<IConverter>().To<CsvConverter>();
        }
    }
}