using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Web.Providers;

namespace Mvc.Web.Converters
{
    public class ConverterFactory : IConverterFactory
    {
        IConverter IConverterFactory.Create(Type provider)
        {
            if (provider == typeof(GoogleProvider))
            {
                return new CsvConverter();
            }
            if (provider == typeof(YahooProvider))
            {
                return new XmlConverter();
            }

            return null;
        }
    }
}