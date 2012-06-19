using System;
using System.Collections.Generic;
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
        private IEnumerable<Quote> quotes = new List<Quote> {
                new Quote { Date = DateTime.Parse("11.06.2012"), Close = 29.09, High = 12.12, Low = 121.12, Open = 35.1, Volume = 1412412 },
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
            conv.Setup(c => c.Convert(providerData)).Returns(quotes);
            actual = conv.Object.Convert(providerData);
            var list = (IList<Quote>)actual;
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(quotes, list);
            Assert.IsTrue(actual.GetEnumerator().MoveNext());
        }
    }
}