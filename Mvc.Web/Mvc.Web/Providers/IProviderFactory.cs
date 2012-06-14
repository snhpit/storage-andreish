using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Web.Providers
{
    public interface IProviderFactory
    {
        IProvider Create(string providerName);
    }
}