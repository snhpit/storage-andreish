using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Web.Providers
{
    public class ProviderFactory : IProviderFactory
    {
        IProvider IProviderFactory.Create(string providerName)
        {
            if (providerName.ToLower() == typeof(GoogleProvider).Name.ToLower())
            {
                return new GoogleProvider();
            }
            if (providerName.ToLower() == typeof(YahooProvider).Name.ToLower())
            {
                return new YahooProvider();
            }
            return null;
        }
    }
}