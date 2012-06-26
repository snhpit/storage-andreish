using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Web.Providers
{
    public static class StaticMethods
    {
        public static void CheckDate(ref DateTime dateFrom, ref DateTime dateTo)
        {
            if (dateFrom.Year == 1)
            {
                dateFrom = DateTime.Now.AddYears(-1);
            }
            if (dateTo.Year == 1)
            {
                dateTo = DateTime.Now;
            }

            if (dateFrom > DateTime.Now)
            {
                dateFrom = DateTime.Now;
            }
            if (dateTo > DateTime.Now)
            {
                dateTo = DateTime.Now;
            }
        }
    }
}