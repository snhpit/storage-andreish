using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Moq;
using Mvc.Web.Providers;

namespace Mvc.Web.Tests
{
    /// <summary>
    ///This is a test class for GoogleProviderTest and is intended
    ///to contain all GoogleProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GoogleProviderTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private static GoogleProvider _provider;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _provider = new GoogleProvider();
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
            Assert.IsTrue(actual.StartsWith("Date"));
            Assert.IsTrue(actual.Length != 0);
        }
    }
}