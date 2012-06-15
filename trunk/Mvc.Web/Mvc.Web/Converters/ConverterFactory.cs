using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Web.Providers;
using Ninject.Syntax;
using Ninject.Web.Mvc;

namespace Mvc.Web.Converters
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly NinjectDependencyResolver _dependencyResolver;

        public ConverterFactory(IResolutionRoot resolutionRoot)
        {
            _dependencyResolver = new NinjectDependencyResolver(resolutionRoot);
        }

        IConverter IConverterFactory.Create(string provider)
        {
            if (provider == "GoogleProvider")
            {
                return (IConverter) _dependencyResolver.GetService(typeof(CsvConverter));
            }
            if (provider == "YahooProvider")
            {
                return (IConverter) _dependencyResolver.GetService(typeof(XmlConverter));
            }

            return null;
        }
    }
}