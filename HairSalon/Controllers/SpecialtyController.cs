using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {

    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/specialties")]
    public ActionResult Create()
    {
      Specialty newSpecialty = new Specialty(Request.Form["new-specialty"]);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return RedirectToAction("Index");
    }

    [HttpGet("/employees/{employeeId}/specialties/{specialtyId}")]
    public ActionResult Details(int employeeId, int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Employee employee = Employee.Find(employeeId);
      model.Add("specialty", specialty);
      model.Add("employee", employee);
      return View(specialty);
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Employee> specialtyEmployee = selectedSpecialty.GetEmployees();
      List<Employee> allEmployee = Employee.GetAll();
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("specialtyEmployee", specialtyEmployee);
      model.Add("allEmployee", allEmployee);
      return View(model);
    }

    [HttpPost("/specialties/{specialtyId}/employees/new")]
    public ActionResult AddEmployee(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Employee newEmployee = Employee.Find(Int32.Parse(Request.Form["employee-id"]));
      specialty.AddEmployee(newEmployee);
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{specialtyId}/delete")]
    public ActionResult DeleteOne(int specialtyId)
    {
      Specialty thisSpecialty = Specialty.Find(specialtyId);
      thisSpecialty.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/specialties/{speicaltyId}/update")]
    public ActionResult UpdateForm(int speicaltyId)
    {
      Specialty thisSpecialty = Specialty.Find(speicaltyId);
      return View(thisSpecialty);
    }

    [HttpPost("/specialties/{speicaltyId}/update")]
    public ActionResult Update(int speicaltyId)
    {
      Specialty thisSpecialty = Specialty.Find(speicaltyId);
      thisSpecialty.Edit(Request.Form["new-specialty-name"]);
      return RedirectToAction("Index");
    }

    [HttpPost("/specialties/delete")]
    public ActionResult DeleteAll()
    {

      Specialty.DeleteAll();
      return View();
    }
  }
}
