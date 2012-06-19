using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Mvc.Web.Converters;
using Ninject;

namespace Mvc.Web.Tests
{
    /// <summary>
    ///This is a test class for ConverterFactoryTest and is intended
    ///to contain all ConverterFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConverterFactoryTest
    {
        private static IConverterFactory _converterFactory;
        private static IKernel _ninjectKernel;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _ninjectKernel = new StandardKernel();
            _converterFactory = new ConverterFactory(_ninjectKernel);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Mvc.Web.Converters.IConverterFactory.Create
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\_Projects\\Mvc.Web\\Mvc.Web", "/")]
        [UrlToTest("http://localhost:53208/")]
        [DeploymentItem("Mvc.Web.dll")]
        public void CreateConverterTest()
        {
            string provider = "GoogleProvider";
            IConverter expected = new CsvConverter();
            IConverter actual;
            actual = _converterFactory.Create(provider);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.AreNotSame(expected, actual);
        }

        [TestMethod()]
        public void CreateConverterReturnNullTest()
        {
            string provider = "SOLOLO";
            IConverter expected = new CsvConverter();
            IConverter actual;
            actual = _converterFactory.Create(provider);
            Assert.IsNull(actual);
        }
    }
}