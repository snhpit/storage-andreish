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
        private IProvider _provider;

        public XmlConverter(IProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<Quote> Convert()
        {
            XDocument document = XDocument.Parse(_provider.GetData());

            if (document.Root == null) return null;

            var xElements = document.Root.Descendants("quote")
                .Select(element => new Quote
                    {
                        Date = DateTime.Parse(element.Element("Date").Value),
                        Open = double.Parse(element.Element("Open").Value),
                        Close = double.Parse(element.Element("Close").Value),
                        Low = double.Parse(element.Element("Low").Value),
                        High = double.Parse(element.Element("High").Value),
                        Volume = double.Parse(element.Element("Volume").Value),
                    });

            return xElements;
        }
    }
}