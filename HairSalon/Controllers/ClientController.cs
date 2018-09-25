using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    [HttpPost("/clients/new")]
    public ActionResult Create(string clientName, int employeeId)
    {
      new Client(clientName, employeeId).Save();
      Client foundClient = Client.Find(employeeId);
      return View("Client/Details", foundClient);
    }


    // [HttpGet("/clients/{clientsId}")]
    // public ActionResult Details(int clientsId)
    // {
    //   return View(Client.Find(clientsId));
    // }ß
    //
    // [HttpPost("/clients/{clientsId}/newClient")]
    // public ActionResult CreateClient(string clientName, int clientsId)
    // {
    //   new Client(clientName, clientsId).Save();
    //   return View("Details", Client.Find(clientsId));
    // }

    // [HttpGet("/clients/{clientsId}/delete")]
    // public ActionResult DeleteClient(int clientsId)
    // {
    //   Client foundClient = Client.Find(clientsId);
    //   foundClient.Delete();
    //   return View();
    // }

    [HttpPost("/clients/delete")]
        public ActionResult DeleteAll()
        {
            Client.DeleteAll();
            return View();
        }

  }
}
