using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpPost("/client/new")]
        public ActionResult Create(string clientName, int stylistId)
        {
            new Client(clientName, stylistId).Save();
            Stylist foundStylist = Stylist.Find(stylistId);
            return View("Stylist/Details", foundStylist);
        }
    }
}
