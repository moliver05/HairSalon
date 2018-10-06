using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using MySql.Data.MySqlClient;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientControllerTests
  {
    public void Index_Returns_ClientList()
    {
        //Arrange
        ViewResult indexView = new ClientController().Index() as ViewResult;

        //Act
        var result = indexView.ViewData.Model;

        //Assert
        Assert.IsInstanceOfType(result, typeof(List<Client>));
    }

    // [TestMethod]
    // public void ClientDetails_Returns_True()
    // {
    //   //Arrange
    //   ViewResult DetailsView = new ClientController().Details() as ViewResult;
    //
    //   //Act
    //   var result = DetailsView.ViewData.Model;
    //
    //   //Assert
    //   Assert.IsInstanceOfType(result, typeof(List<Client>));
    // }
  }
}
