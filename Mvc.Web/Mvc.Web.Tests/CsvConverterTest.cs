using Mvc.Web.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Mvc.Entities;
using System.Collections.Generic;

namespace Mvc.Web.Tests
{
    /// <summary>
    ///This is a test class for CsvConverterTest and is intended
    ///to contain all CsvConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CsvConverterTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Convert
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\_Projects\\Mvc.Web\\Mvc.Web", "/")]
        [UrlToTest("http://localhost:53208/")]
        public void ConvertTest()
        {
            CsvConverter target = new CsvConverter(); // TODO: Initialize to an appropriate value
            string providerData = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<Quote> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Quote> actual;
            actual = target.Convert(providerData);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
