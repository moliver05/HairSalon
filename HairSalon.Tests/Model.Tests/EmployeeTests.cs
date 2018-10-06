using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Test
{
  [TestClass]
  public class EmployeeTests : IDisposable
  {
    public void Dispose()
    {
      Employee.DeleteAll();
      Specialty.DeleteAll();
      Client.DeleteAll();
    }
    public EmployeeTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_test;";
    }

    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Employee.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_ToDatabase_EmployeeList()
    {
      //Arrange
      Employee testEmployee = new Employee("Name", 1);
      testEmployee.Save();

      //Act
      List<Employee> result = Employee.GetAll();
      List<Employee> testList = new List<Employee>{testEmployee};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

  }
}
