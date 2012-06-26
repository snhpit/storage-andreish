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
    ///This is a test class for CsvConverterTest and is intended
    ///to contain all CsvConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CsvConverterTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        private IEnumerable<Quote> _quotes = new List<Quote> {
                new Quote { Date = DateTime.Parse("18-Jun-12"), Close = 29.84, High = 30.03, Low = 29.71, Open = 29.99, Volume = 58285251 },
            };
   
        public TestContext TestContext { get; set; }

        private static CsvConverter _converter;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _converter = new CsvConverter();
        }

        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            string providerData = "Date,Open,High,Low,Close,Volume\n18-Jun-12,29.99,30.03,29.71,29.84,58285251\n";
            IEnumerable<Quote> actual;
            Mock<IConverter> conv = new Mock<IConverter>();
            conv.Setup(c => c.Convert(providerData)).Returns(_quotes);
            var fakeQuotes = conv.Object.Convert(providerData);
            var converter = new CsvConverter();
            var realQuotes = converter.Convert(providerData);
                                             
            Assert.AreNotEqual(null, realQuotes.FirstOrDefault());
            Assert.AreEqual(realQuotes.Count(), fakeQuotes.Count());
            var f = realQuotes.Select(quote => quote.Close);
            var q = fakeQuotes.Select(quote => quote.Close);
            Assert.AreEqual(realQuotes.Select(quote => quote.Close).First(), fakeQuotes.Select(quote => quote.Close).First());
            Assert.IsTrue(realQuotes.All(quote => quote.Volume > default(double)));
        }
    }
}