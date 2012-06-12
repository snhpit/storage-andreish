using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Mvc.Web.Controllers;
using Mvc.Web.Converters;
using Mvc.Web.Providers;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
//using Ninject.Web.Mvc;

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
            var providerFactoryMethod = new Func<string, IProvider>(provider =>
                {
                    switch (provider)
                    {
                        case "Google":
                            return _ninjectKernel.Get<GoogleProvider>();
                        case "Yahoo":
                            return _ninjectKernel.Get<YahooProvider>();
                        default:
                            return _ninjectKernel.Get<GoogleProvider>();
                    }
                });
            
            var converterFactoryMethod = new Func<string, IConverter>(converter =>
            {
                switch (converter)
                {
                    case "Csv":
                        return _ninjectKernel.Get<CsvConverter>();
                    case "Xml":
                        return _ninjectKernel.Get<XmlConverter>();
                    default:
                        return _ninjectKernel.Get<CsvConverter>();
                }
            });
            _ninjectKernel.Bind<IProvider>().To<YahooProvider>();
            //var h1 = _ninjectKernel.Bind<Func<string, IProvider>>().ToConstant(providerFactoryMethod);//.To<GoogleProvider>();
            //var h2 = _ninjectKernel.Bind<Func<string, IConverter>>().ToConstant(converterFactoryMethod);
            //var t = ServiceLocator.Current.GetInstance<YahooProvider>();
            //var q = _ninjectKernel.Get<GoogleProvider>();
            _ninjectKernel.Bind<IConverter>().To<XmlConverter>();
            //_ninjectKernel.Bind<IConverter>().To<CsvConverter>();
        }
    }
}