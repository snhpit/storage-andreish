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

        IConverter IConverterFactory.Create(Type provider)
        {
            if (provider == typeof(GoogleProvider))
            {
                return _dependencyResolver.GetService(typeof(CsvConverter)) as CsvConverter;
            }
            if (provider == typeof(YahooProvider))
            {
                return _dependencyResolver.GetService(typeof(XmlConverter)) as XmlConverter;
            }

            return null;
        }
    }
}