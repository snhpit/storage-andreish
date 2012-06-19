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
        [TestMethod()]
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
            IConverter actual;
            actual = _converterFactory.Create(provider);
            Assert.IsNull(actual);
        }
    }
}