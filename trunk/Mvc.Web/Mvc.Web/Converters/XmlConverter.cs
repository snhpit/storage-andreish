using System;
using System.Collections.Generic;
using System.Configuration;
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

            if (document.Root == null) return null;

            var xQuotes = document.Root.Descendants("quote");
            var quotes = new List<Quote>();
            foreach (var xQuote in xQuotes)
            {
                var date = xQuote.Element("Date").Value;//DateTime.Parse(xQuote.Element("Date").Value).Date.ToShortDateString();
                var quote = new Quote
                                {
                                    Date = date,
                                    Open = double.Parse(xQuote.Element("Open").Value),
                                    Close = double.Parse(xQuote.Element("Close").Value),
                                    Low = double.Parse(xQuote.Element("Low").Value),
                                    High = double.Parse(xQuote.Element("High").Value),
                                    Volume = double.Parse(xQuote.Element("Volume").Value),
                                };
                quotes.Add(quote);
            }
            //.Select(quote => new Quote
            //    {
            //        Date = DateTime.Parse(quote.Element("Date").Value),
            //        Open = double.Parse(quote.Element("Open").Value),
            //        Close = double.Parse(quote.Element("Close").Value),
            //        Low = double.Parse(quote.Element("Low").Value),
            //        High = double.Parse(quote.Element("High").Value),
            //        Volume = double.Parse(quote.Element("Volume").Value),
            //    });

            return quotes;
        }
    }
}