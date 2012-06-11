using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Entities;

namespace Mvc.Web.Providers
{
    public interface IProvider
    {
        //string ProviderName { get; set; }

        string GetData(DateTime dateFrom, DateTime dateTo, string company);
    }
}