using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HairSalon.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Returns_True()
        {
          //
            HomeController controller = new HomeController();
            ActionResult indexView = controller.Index();
            // Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
    }
}
