using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Test
{

    [TestClass]
    public class HomeControllerTest
    {


        [TestMethod]
        public void Error_ReturnsErrorView()
        {
            // Arrange
            var controller = new HomeController();
            var errorView = "~/Views/Shared/Error.cshtml";

            // Act
            var viewResult = controller.Error() as ViewResult;

            // Assert
            Assert.AreEqual(errorView, viewResult.ViewName);
        }


        [TestMethod]
        public void About_ReturnsViewData()
        {
            // Arrange
            var controller = new HomeController();
            var viewData = "Your application description page.";

            // Act
            var viewResult = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual(viewData, viewResult.ViewData["Message"]);
        }




    }
}
