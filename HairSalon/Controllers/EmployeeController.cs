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
    List<Employee> allEmployee = Employee.GetAll();
    return View(allEmployee);
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
      List<Employee> allEmployee = Employee.GetAll();
      return RedirectToAction("Index");
  }

  [HttpGet("/employees/{id}")]
  public ActionResult Details(int id)
  {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Employee selectedEmployee = Employee.Find(id);
      List<Client> employeeClient = selectedEmployee.GetClients();
      model.Add("employee", selectedEmployee);
      model.Add("client", employeeClient);
      return View(model);
  }

  [HttpPost("/employees/{id}")]
  public ActionResult CreateClient(string clientName, int employeeId)
  {
     Dictionary<string, object> model = new Dictionary<string, object>();
     Employee foundEmployee = Employee.Find(employeeId);
     Client newClient = new Client(Request.Form["new-client"], employeeId);
     newClient.Save();
     List<Client> employeeClient = foundEmployee.GetClients();
     model.Add("client", employeeClient);
     model.Add("employee", foundEmployee);
     return RedirectToAction("Details");
   }

  [HttpPost("/employees/delete")]
  public ActionResult DeleteAll()
  {

    Employee.DeleteAll();
    return View();
  }

  [HttpGet("/employees/{clientId}/delete")]
  public ActionResult Delete(int clientId)
  {
    Client thisClient = Client.Find(clientId);
    thisClient.Delete();
    return RedirectToAction("Index");
  }

  [HttpGet("/employees/{employeeId}/update")]
  public ActionResult UpdateForm(int employeeId)
  {
    Employee thisEmployee = Employee.Find(employeeId);
    return View(thisEmployee);
  }

  [HttpPost("/employees/{employeeId}/update")]
  public ActionResult Update(int employeeId)
  {
    Employee thisEmployee = Employee.Find(employeeId);
    thisEmployee.Edit(Request.Form["new-employee"]);
    return RedirectToAction("Index");
  }

 }
}
