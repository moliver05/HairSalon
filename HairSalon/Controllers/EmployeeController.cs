using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class EmployeeController : Controller
  {
    [HttpPost("/employee/new")]
    public ActionResult CreateEmployee(string employeeName)
    {
      new Employee(employeeName).Save();
      return RedirectToAction("Index", "Home");
    }

    [HttpGet("/employee/{employeeId}")]
    public ActionResult Details(int employeeId)
    {
      return View(Employee.Find(employeeId));
    }

    [HttpPost("/employee/{employeeId}/newClient")]
    public ActionResult CreateClient(string clientName, int employeeId)
    {
      new Client(clientName, employeeId).Save();
      return View("Details", Employee.Find(employeeId));
    }

    [HttpGet("/employee/{employeeId}/delete")]
    public ActionResult DeleteEmployee(int employeeId)
    {
      Employee foundEmployee = Employee.Find(employeeId);
      foundEmployee.Delete();
      return RedirectToAction("Index", "Home");
    }
  }
}
