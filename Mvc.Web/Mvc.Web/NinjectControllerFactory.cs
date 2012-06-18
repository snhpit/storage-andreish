using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Mvc.Web.Controllers;
using Mvc.Web.Converters;
using Mvc.Web.Providers;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using Ninject.Web.Mvc;
using NinjectAdapter;

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
            _ninjectKernel.Bind<IProviderFactory>().To<ProviderFactory>();
            _ninjectKernel.Bind<IConverterFactory>().To<ConverterFactory>();

            //var q = DependencyResolver.Current.GetType().GetMethod("GetService")
            //    .Invoke(DependencyResolver.Current, new[] { Type.GetType("Mvc.Web.Providers.YahooProvider") });
        }
    }
}