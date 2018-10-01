using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
public class SpecialtyController : Controller
{
  [HttpGet("/specialties")]
  public ActionResult Index()
  {
    List<Specialty> allSpecialty = Specialty.GetAll();
    return View(allSpecialty);
  }

  [HttpGet("/specialties/new")]
  public ActionResult CreateForm()
  {

    return View();
  }

  [HttpPost("/specialties")]
  public ActionResult Create()
  {
    Specialty newSpecialty = new Specialty(Request.Form["specialty-name"]);
    newSpecialty.Save();
    return RedirectToAction("Index");
  }

  [HttpGet("/specialties/{id}")]
  public ActionResult Details(int id)
  {
    Dictionary<string, object> model = new Dictionary<string, object>();
    Specialty selectedSpecialty = Specialty.Find(id);
    List<Employee> employeeSpecialty = selectedSpecialty.GetEmployee();
    List<Employee> allEmployee = Employee.GetAll();
    model.Add("selectedSpecialty", selectedSpecialty);
    model.Add("employeeSpecialty", employeeSpecialty);
    model.Add("allEmployee", allEmployee);
    return View(model);
  }

  [HttpGet("/specialties/{id}/employees/new")]
  public ActionResult CreateEmployeeForm()
  {
    return View("~/Views/Employee/CreateForm.cshtml");
  }

  [HttpPost("/specialties/{specialtyId}/employees/new")]
  public ActionResult AddEmployee(int specialtyId)
  {
    Specialty specialty = Specialty.Find(specialtyId);
    Employee employee = Employee.Find(Int32.Parse(Request.Form["employee-id"]));
    specialty.AddEmployee(employee);
    return RedirectToAction("Index");
  }

  [HttpGet("/specialties/{specialtyId}/update")]
  public ActionResult UpdateForm(int specialtyId)
  {
     Specialty thisSpecialty = Specialty.Find(specialtyId);
     return View(thisSpecialty);
  }

  [HttpPost("/specialties/{specialtyId}/update")]
  public ActionResult Update(int specialtyId)
  {
    Specialty thisSpecialty = Specialty.Find(specialtyId);
    thisSpecialty.Edit(Request.Form["new-specialty-name"]);
    return RedirectToAction("Index");
  }

  [HttpGet("/specialties/{specialtyid}/delete")]
  public ActionResult DeleteOne(int specialtyId)
  {
    Specialty thisSpecialty = Specialty.Find(specialtyId);
    thisSpecialty.Delete();
    return RedirectToAction("Index");
  }
}
}
