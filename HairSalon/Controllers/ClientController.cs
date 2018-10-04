using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClient = Client.GetAll();
      return View("Index", allClient);
    }

    [HttpGet("/employees/{employeeId}/clients/new")]
    public ActionResult CreateForm(int employeeid)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Employee employee = Employee.Find(employeeid);
      return View(employee);
    }

    [HttpGet("/employees/{employeeId}/clients/{clientId}")]
    public ActionResult Details(int employeeId, int clientId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Employee employee = Employee.Find(employeeId);
      model.Add("client", client);
      model.Add("employee", employee);
      return View(model);
    }

    [HttpGet("/clients/{clientId}/update")]
    public ActionResult UpdateForm(int clientId)
    {
      Specialty thisClient = Specialty.Find(clientId);
      return View(thisClient);
    }

    [HttpPost("/clients/{clientId}/update")]
    public ActionResult Update(int clientId)
    {
      Specialty thisClient = Specialty.Find(clientId);
      thisClient.Edit(Request.Form["new-client"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/employees/employees/{employeeId}/delete")]
    public ActionResult DeleteAll()
    {
      Client.DeleteAll();
      return View();
    }

    [HttpGet("/clients/{clientId}/delete")]
    public ActionResult Delete(int clientId)
    {
      Client thisClient = Client.Find(clientId);
      thisClient.Delete();
      return RedirectToAction("Index");
    }

 }
}
