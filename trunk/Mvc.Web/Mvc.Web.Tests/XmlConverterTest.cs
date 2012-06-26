using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Moq;
using Mvc.Entities;
using Mvc.Web.Converters;

namespace Mvc.Web.Tests
{
    /// <summary>
    ///This is a test class for XmlConverterTest and is intended
    ///to contain all XmlConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlConverterTest
    {
        private IEnumerable<Quote> _quotes = new List<Quote> {
                new Quote { Date = DateTime.Parse("18-Jun-12"), Close = 29.84, High = 30.03, Low = 29.71, Open = 29.99, Volume = 58285251 },
            };
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private static XmlConverter _converter;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _converter = new XmlConverter();
        }

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            var providerData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<query xmlns:yahoo=\"http://www.yahooapis.com/v1/base.rng\" yahoo:count=\"253\" yahoo:created=\"2012-06-20T08:45:08Z\" yahoo:lang=\"en-US\"><results><quote date=\"2012-06-19\"><Date>2012-06-19</Date><Open>30.19</Open><High>31.11</High><Low>30.05</Low><Close>30.70</Close><Volume>75714400</Volume><Adj_Close>30.70</Adj_Close></quote></results></query><!-- total: 1599 -->\n<!-- engine4.yql.ch1.yahoo.com -->\n";
            IEnumerable<Quote> actual;
            Mock<IConverter> conv = new Mock<IConverter>();
            conv.Setup(c => c.Convert(providerData)).Returns(_quotes);
            var fakeQuotes = conv.Object.Convert(providerData);
            var converter = new XmlConverter();
            var realQuotes = converter.Convert(providerData);

            Assert.AreNotEqual(null, realQuotes.FirstOrDefault());
            Assert.AreEqual(realQuotes.Count(), fakeQuotes.Count());
            Assert.IsTrue(realQuotes.All(quote => quote.Volume > default(double)));
        }
    }
}