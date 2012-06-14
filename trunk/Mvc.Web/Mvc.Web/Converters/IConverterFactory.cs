using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Web.Converters
{
    public interface IConverterFactory
    {
        IConverter Create(Type provider);
    }
}