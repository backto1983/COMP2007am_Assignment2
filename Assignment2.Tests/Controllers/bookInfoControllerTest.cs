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
        List<bookInfo> books;
        Mock<IBookInfoRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // Instantiate new mock object
            mock = new Mock<IBookInfoRepository>();

            // Mock bookInfo data
            books = new List<bookInfo>
            {
                new bookInfo { bookID = 1, bookName = "Neuromancer", bookGenre = "Sci-Fi" },
                new bookInfo { bookID = 2, bookName = "2001: a Space Odyssey", bookGenre = "Sci-Fi" },
                new bookInfo { bookID = 3, bookName = "A Game of Thrones", bookGenre = "Fantasy" },
            };

            // Populate the mock repository with mock data
            mock.Setup(b => b.books).Returns(books.AsQueryable());

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
        public void IndexLoadsBooks()
        {
            // Act - cast ActionResult return type to ViewResult to access the model
            var actual = (List<bookInfo>)((ViewResult)controller.Index()).Model;

            //List<bookInfo> actual = books.ToList();

            // Assert
            CollectionAssert.AreEqual(books, actual);
        }

        [TestMethod]
        public void IndexSearch(String Title) // Another method with an inexistent book
        {
            Title = "Neuromancer";

            // Act - cast ActionResult return type to ViewResult to access the model
            var actual = (List<bookInfo>)((ViewResult)controller.Index()).Model;

            // Assert
            CollectionAssert.Contains(actual, Title);
        }

        [TestMethod]
        public void DetailsValidID()
        {
            // Act
            var actual = ((ViewResult)controller.Details(1)).Model;

            // Assert 
            Assert.AreEqual(books[0], actual);
        }

        //GET: Details
        [TestMethod]
        public void DetailsInvalidId()
        {
            // Act
            var actual = (ViewResult)controller.Details(4);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsNoId()
        {
            // Act
            var actual = (ViewResult)controller.Details(null);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Edit
        [TestMethod]
        public void EditGetValidID()
        {
            // Act
            var actual = ((ViewResult)controller.Edit(1)).Model;

            // Assert
            Assert.AreEqual(books[0], actual);
        }

        [TestMethod]
        public void EditGetInvalidID()
        {
            // Act
            var actual = (ViewResult)controller.Edit(4);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditGetNoID()
        {
            // Assert - must pass an int so the overload calls GET not POST
            int? id = null;

            // Act
            var actual = (ViewResult)controller.Edit(id);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // POST: Edit
        [TestMethod]
        public void EditPostValID()
        {
            // Act - pass in the first mock books object
            var actual = (RedirectToRouteResult)controller.Edit(books[0]);

            // Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPostInvalID()
        {
            // Arrange - manually set the model state to invalid
            controller.ModelState.AddModelError("key", "unit test error");

            // Act - pass in the first mock books object
            var actual = (ViewResult)controller.Edit(books[0]);

            // Assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        // GET: Create
        [TestMethod]
        public void CreateViewLoads()
        {
            // Act
            var actual = (ViewResult)controller.Create();

            // Assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        [TestMethod]
        public void CreatePostValID()
        {
            // Arrange
            bookInfo b = new bookInfo
            {
                bookName = "A Game of Thrones",
                bookGenre = "Fantasy"
            };

            // Act
            var actual = (RedirectToRouteResult)controller.Create(b);

            // Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreatePostInvalID()
        {
            // arrange
            bookInfo b = new bookInfo
            {
                bookName = "A Game of Thrones",
                bookGenre = "Fantasy"
            };

            controller.ModelState.AddModelError("key", "cannot add order");

            // Act
            var actual = (ViewResult)controller.Create(b);

            // Assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // GET: Delete
        [TestMethod]
        public void DeleteValidID()
        {
            // Act
            var actual = ((ViewResult)controller.Delete(1)).Model;

            // Assert
            Assert.AreEqual(books[0], actual);
        }

        [TestMethod]
        public void DeleteInvalidID()
        {
            // Act
            var actual = (ViewResult)controller.Delete(4);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteNoID()
        {
            // Act
            var actual = (ViewResult)controller.Delete(null);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // POST: Delete
        [TestMethod]
        public void DeletePostValid()
        {
            // Act
            var actual = (RedirectToRouteResult)controller.DeleteConfirmed(34);

            // Assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }
    }
}
