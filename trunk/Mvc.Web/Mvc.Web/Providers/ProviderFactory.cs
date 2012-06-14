using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Syntax;
using Ninject.Web.Mvc;

namespace Mvc.Web.Providers
{
    public class ProviderFactory : IProviderFactory
    {
        private readonly NinjectDependencyResolver _dependencyResolver;
        public ProviderFactory(IResolutionRoot resolutionRoot)
        {
            _dependencyResolver = new NinjectDependencyResolver(resolutionRoot);
        }

        IProvider IProviderFactory.Create(string providerName)
        {
            if (providerName == null) { return null; }
            if (providerName.ToLower() == typeof(GoogleProvider).Name.ToLower())
            {
                return _dependencyResolver.GetService(typeof(GoogleProvider)) as GoogleProvider;
            }
            if (providerName.ToLower() == typeof(YahooProvider).Name.ToLower())
            {
                return _dependencyResolver.GetService(typeof(YahooProvider)) as YahooProvider;
            }
            return null;
        }
    }
}