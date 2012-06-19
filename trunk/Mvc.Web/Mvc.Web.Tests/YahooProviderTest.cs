using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Moq;
using Mvc.Web.Providers;

namespace Mvc.Web.Tests
{
    /// <summary>
    ///This is a test class for YahooProviderTest and is intended
    ///to contain all YahooProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class YahooProviderTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private static YahooProvider _provider;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _provider = new YahooProvider();
        }

        /// <summary>
        ///A test for GetData
        ///</summary>
        [TestMethod()]
        public void GetDataTest()
        {
            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();
            string company = "MSFT";
            string expected = string.Empty;
            string actual;
            actual = _provider.GetData(dateFrom, dateTo, company);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.StartsWith(@"<?xml"));
            //var provider = new Mock<IProvider>();
            //provider.Verify(p => p.GetData(dateFrom, dateTo, company), Times.Once());
            //provider.Setup(p => p.GetData(dateFrom, dateTo, "!fadf/;1")).Throws<NullReferenceException>();
        }
    }
}