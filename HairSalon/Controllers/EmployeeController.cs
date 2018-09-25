using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class EmployeeController : Controller
  {
    [HttpGet("/employees")]
    public ActionResult Index()
    {
      List<Employee> allEmployee = Employee.GetAll();
      return View(allEmployee);
    }

    [HttpGet("/employees/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    // [HttpGet("/employees")]
    // public ActionResult Create(string employeeName, int employeeId)
    // {
    //   Employee newEmployee = Employee(employeeName, employeeId);
    //   newEmployee.Save();
    //   return RedirectToAction("Index");
    // }

    [HttpPost("/employees/{employeeId}")]
    public ActionResult Create()
    {
      List<Employee> allEmployee = Employee.GetAll();
      return View(allEmployee);
    }

    [HttpPost("/employees/{employeeId}/newClient")]
    public ActionResult CreateClient(string clientName, int employeeId)
    {
      new Client(clientName, employeeId).Save();
      return View("Details", Employee.Find(employeeId));
    }

    [HttpGet("/employees/{employeeId}/delete")]
    public ActionResult DeleteEmployee(int employeeId)
    {
      Employee foundEmployee = Employee.Find(employeeId);
      foundEmployee.Delete();
      return View("Index");
    }

    [HttpPost("/employees/delete")]
        public ActionResult DeleteAll()
        {
            Employee.DeleteAll();
            return View();
        }

  }
}
