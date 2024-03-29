﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using Assignment2.Controllers;

namespace Assignment2.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        HomeController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // Instantiate the HomeController declared at the class level
            controller = new HomeController();
        }

        [TestMethod]
        public void Index()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
