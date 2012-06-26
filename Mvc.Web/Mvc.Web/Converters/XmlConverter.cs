using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Mvc.Entities;
using Mvc.Web.Providers;

namespace Mvc.Web.Converters
{
    public class XmlConverter : IConverter
    {
        public IEnumerable<Quote> Convert(string providerData)
        {
            XDocument document = XDocument.Parse(providerData);

            if (document.Root == null || document.Root.Value == String.Empty)
            {
                throw new NullReferenceException("XmlConverter");
            }

            var quotes = document.Root.Descendants("quote").Select(quote => new Quote
                {
                    Date = DateTime.Parse(quote.Element("Date").Value, new CultureInfo("en-US", true)),
                    Open = double.Parse(quote.Element("Open").Value),
                    Close = double.Parse(quote.Element("Close").Value),
                    Low = double.Parse(quote.Element("Low").Value),
                    High = double.Parse(quote.Element("High").Value),
                    Volume = double.Parse(quote.Element("Volume").Value),
                });

            return quotes;
        }
    }
}