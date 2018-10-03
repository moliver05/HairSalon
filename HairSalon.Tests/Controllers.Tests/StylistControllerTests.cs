using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistsControllerTests
  {
    [TestMethod]
    public void Index_Returns_True()
    {
      StylistsController controller = new StylistsController();
      ActionResult indexView = controller.Index();

      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }
    [TestMethod]
    public void CreateForm_Returns_True()
    {
      StylistsController controller = new StylistsController();
      ActionResult createFormView = controller.CreateForm();

      Assert.IsInstanceOfType(createFormView, typeof(ViewResult));
    }
    [TestMethod]
    public void StylistDetails_Returns_True()
    {
      StylistsController controller = new StylistsController();
      ActionResult DetailsView = controller.Details();

      Assert.IsInstanceOfType(DetailsView, typeof(ViewResult));
    }
  }
}
