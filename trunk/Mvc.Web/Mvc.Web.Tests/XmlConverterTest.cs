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
    ///This is a test class for XmlConverterTest and is intended
    ///to contain all XmlConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlConverterTest
    {
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
            string providerData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<query xmlns:yahoo=\"http://www.yahooapis.com/v1/base.rng\" yahoo:count=\"0\" yahoo:created=\"2012-06-18T23:06:40Z\" yahoo:lang=\"en-US\"><results/></query><!-- total: 300 -->\n<!-- engine3.yql.ch1.yahoo.com -->\n";
            IEnumerable<Quote> actual;
            //actual = _converter.Convert(providerData);
            //Assert.AreNotEqual(null, actual);
            Mock<IConverter> conv = new Mock<IConverter>();
            conv.Setup(c => c.Convert(providerData)).Throws<NullReferenceException>();
            //var f = conv.Object.Convert(providerData);
        }
    }
}