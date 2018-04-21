using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Additional references
using Assignment2.Controllers;
using System.Web.Mvc;
using Moq;
using Assignment2.Models;
using System.Linq;

namespace Assignment2.Tests.Controllers
{

    [TestClass]
    public class bookInfoControllerTest
    {
        bookInfoController controller;
        List<bookInfo> booksInfo;
        Mock<IBookInfoRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // Instantiate new mock object
            mock = new Mock<IBookInfoRepository>();

            // Mock bookInfo data
            booksInfo = new List<bookInfo>
            {
                new bookInfo { bookID = 1, bookName = "Ubik", bookGenre = "Sci-Fi" },
                new bookInfo { bookID = 2, bookName = "A Game of Thrones", bookGenre = "Fantasy" }
            };

            // Populate the mock repository with mock data
            mock.Setup(b => b.bookInfo).Returns(booksInfo.AsQueryable());

            // Inject the mock dependency when calling the controller's constructor
            controller = new bookInfoController(mock.Object);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexLoadsBooksInfo()
        {
            // Act - cast ActionResult return type to ViewResult to access the model
            var actual = (List<bookInfo>)((ViewResult)controller.Index()).Model;

            // Assert
            CollectionAssert.AreEqual(booksInfo, actual);
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
