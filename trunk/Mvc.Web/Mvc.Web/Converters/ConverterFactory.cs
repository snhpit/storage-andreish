using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Web.Providers;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Mvc;

namespace Mvc.Web.Converters
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly NinjectDependencyResolver _dependencyResolver;

        public ConverterFactory(IKernel resolutionRoot)
        {
            _dependencyResolver = new NinjectDependencyResolver(resolutionRoot);
        }

        IConverter IConverterFactory.Create(string providerName)
        {
            if (providerName.ToLower() == "googleprovider")
            {
                return (IConverter)_dependencyResolver.GetService(typeof(CsvConverter));
            }
            if (providerName.ToLower() == "yahooprovider")
            {
                return (IConverter)_dependencyResolver.GetService(typeof(XmlConverter));
            }

            return null;
        }
    }
}