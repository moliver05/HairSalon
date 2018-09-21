using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    [HttpPost("/client/new")]
    public ActionResult Create(string clientName, int employeeId)
    {
      new Client(clientName, employeeId).Save();
      Stylist foundEmployee = Employee.Find(employeeId);
      return View("Employee/Details", foundEmployee);
    }
  }
}
