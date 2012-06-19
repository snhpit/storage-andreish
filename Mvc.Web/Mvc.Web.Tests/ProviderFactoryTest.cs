using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Mvc.Web.Providers;
using Ninject;

namespace Mvc.Web.Tests
{
    /// <summary>
    ///This is a test class for ProviderFactoryTest and is intended
    ///to contain all ProviderFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProviderFactoryTest
    {
        private static IProviderFactory _providerFactory;
        private static IKernel _ninjectKernel;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _ninjectKernel = new StandardKernel();
            _providerFactory = new ProviderFactory(_ninjectKernel);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Mvc.Web.Providers.IProviderFactory.Create
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\_Projects\\Mvc.Web\\Mvc.Web", "/")]
        [UrlToTest("http://localhost:53208/")]
        [DeploymentItem("Mvc.Web.dll")]
        public void CreateProviderTest()
        {
            string providerName = "YahooProvider";
            IProvider expected = new YahooProvider();
            IProvider actual;
            actual = _providerFactory.Create(providerName);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.IsInstanceOfType(actual, typeof(YahooProvider));
            Assert.AreNotSame(expected, actual);
        }

        [TestMethod]
        public void CreateProviderTestReturnNull()
        {
            string providerName = "O";
            IProvider actual;
            actual = _providerFactory.Create(providerName);
            Assert.IsNull(actual);
        }
    }
}