using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class EmployeeControllerTest
    {
      [TestMethod]
        public void Index_HasCorrectModelType_EmployeeList()
        {
            //Arrange
            ViewResult indexView = new EmployeeController().Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Employee>));
        }
    }
}
