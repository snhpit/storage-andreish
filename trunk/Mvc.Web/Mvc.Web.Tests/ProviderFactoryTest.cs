using Mvc.Web.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
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
        public void CreateTest()
        {
            IKernel resolutionRoot = null; // TODO: Initialize to an appropriate value
            IProviderFactory target = new ProviderFactory(resolutionRoot); // TODO: Initialize to an appropriate value
            string providerName = string.Empty; // TODO: Initialize to an appropriate value
            IProvider expected = null; // TODO: Initialize to an appropriate value
            IProvider actual;
            actual = target.Create(providerName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
