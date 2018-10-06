using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using MySql.Data.MySqlClient;


namespace HairSalon.Tests
{
  [TestClass]
  public class EmployeeControllerTests
  {

    [TestMethod]
          public void Index_Returns_EmployeeList()
          {
              //Arrange
              ViewResult indexView = new EmployeeController().Index() as ViewResult;

              //Act
              var result = indexView.ViewData.Model;

              //Assert
              Assert.IsInstanceOfType(result, typeof(List<Employee>));
          }

          // [TestMethod]
          // public void EmployeeDetails_Returns_True()
          // {
          //   //Arrange
          //   ViewResult DetailsView = new EmployeeController().Details() as ViewResult;
          //
          //   //Act
          //   var result = DetailsView.ViewData.Model;
          //
          //   //Assert
          //   Assert.IsInstanceOfType(result, typeof(List<Employee>));
          // }
  }
}
