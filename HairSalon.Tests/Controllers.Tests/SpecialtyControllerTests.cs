using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using MySql.Data.MySqlClient;


namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtyControllerTest
    {
      public void Index_Returns_SpecialtyList()
      {
          //Arrange
          ViewResult indexView = new SpecialtyController().Index() as ViewResult;

          //Act
          var result = indexView.ViewData.Model;

          //Assert
          Assert.IsInstanceOfType(result, typeof(List<Specialty>));
      }

        // [TestMethod]
        // public void SpecialtyDetails_Returns_True()
        // {
        //   SpecialtyController controller = new SpecialtyController();
        //   ActionResult DetailsView = controller.Details();
        //
        //   Assert.IsInstanceOfType(DetailsView, typeof(ViewResult));
        // }
      }
    }
