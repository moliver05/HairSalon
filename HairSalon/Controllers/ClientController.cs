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
    public ActionResult CreateForm(int employeeId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Employee employee = Employee.Find(employeeId);
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
    [HttpGet("/employees/employees/{employeeId}/delete")]
    public ActionResult DeleteAll()
    {

      Client.DeleteAll();
      return View();
    }

 }
}
