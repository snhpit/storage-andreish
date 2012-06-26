using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using Mvc.Entities;
using Mvc.Web.Providers;

namespace Mvc.Web.Converters
{
    public class CsvConverter : IConverter
    {
        public IEnumerable<Quote> Convert(string providerData)
        {
            var data = providerData.Split('\n').TakeWhile(elem => !string.IsNullOrWhiteSpace(elem)).Skip(1);

            if (!data.Any())
            {
                throw new NullReferenceException("CsvConverter");
            }

            var quotes = data.Select(line => line.Split(','))
                .Select(quote => new Quote
                    {
                        Date = DateTime.Parse(quote[0], new CultureInfo("en-US", true)),
                        Open = double.Parse(quote[1]),
                        High = double.Parse(quote[2]),
                        Low = double.Parse(quote[3]),
                        Close = double.Parse(quote[4]),
                        Volume = double.Parse(quote[5]),
                    });

            return quotes;
        }
    }
}