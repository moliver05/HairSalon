using System;
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
    List<Employee> allEmployees = Employee.GetAll();
    return View(allEmployees);
  }

  [HttpGet("/employees/new")]
  public ActionResult CreateForm()
  {
      return View();
  }

  [HttpPost("/employees")]
  public ActionResult Create()
  {
      Employee newEmployee = new Employee(Request.Form["new-name"]);
      newEmployee.Save();
      List<Employee> allEmployees = Employee.GetAll();
      return RedirectToAction("Index");
  }

  [HttpGet("/employees/{id}")]
  public ActionResult Details(int id)
  {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Employee selectedEmployee = Employee.Find(id);
      List<Client> employeeClients = selectedEmployee.GetClients();
      model.Add("employee", selectedEmployee);
      model.Add("client", employeeClients);
      return View(model);
  }

  [HttpPost("/employees/{id}")]
  public ActionResult CreateClient(string clientName, int employeeId)
  {
     Dictionary<string, object> model = new Dictionary<string, object>();
     Employee foundEmployee = Employee.Find(employeeId);
     Client newClient = new Client(Request.Form["new-client"], employeeId);
     newClient.Save();
     List<Client> employeeClients = foundEmployee.GetClients();
     model.Add("client", employeeClients);
     model.Add("employee", foundEmployee);
     return RedirectToAction("Details");
   }

  [HttpPost("/employees/delete")]
  public ActionResult DeleteAll()
  {
    Employee.DeleteAll();
    return View();
  }

 }
}
