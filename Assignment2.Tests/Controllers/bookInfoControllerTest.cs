using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Additional references
using Assignment2.Controllers;
using System.Web.Mvc;

namespace Assignment2.Tests.Controllers
{

    [TestClass]
    public class bookInfoControllerTest
    {
        bookInfoController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // Instantiate the HomeController declared ta the class level
            controller = new bookInfoController();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        public bookInfoControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
    }
}
