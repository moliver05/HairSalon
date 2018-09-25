using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class EmployeeController : Controller
  {
    [HttpGet("/employee")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/employee/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/employee")]
    public ActionResult Create()
    {
      List<Employee> allEmployee = Employee.GetAll();
      return View("Index");
    }

    [HttpPost("/employee/{employeeId}")]
    public ActionResult Create()
    {
      List<Employee> allEmployee = Employee.GetAll();
      return View(allEmployee);
    }

    [HttpPost("/employee/{employeeId}/newClient")]
    public ActionResult CreateClient(string clientName, int employeeId)
    {
      new Client(clientName, employeeId).Save();
      return View("Details", Employee.Find(employeeId));
    }

    // [HttpGet("/employee/{employeeId}/delete")]
    // public ActionResult DeleteEmployee(int employeeId)
    // {
    //   Employee foundEmployee = Employee.Find(employeeId);
    //   foundEmployee.Delete();
    //   return View("Index");
    // }
    //
    [HttpPost("/employees/delete")]
        public ActionResult DeleteAll()
        {
            Employee.DeleteAll();
            return View();
        }

  }
}
