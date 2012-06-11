using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Entities;

namespace Mvc.Web.Converters
{
    public interface IConverter
    {
        IEnumerable<Quote> Convert(string providerData);
    }
}