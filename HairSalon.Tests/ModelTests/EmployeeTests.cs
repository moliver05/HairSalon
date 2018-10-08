using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Test
{
  [TestClass]
    public class EmployeeTests : IDisposable
    {
        public void Dispose()
        {
            Employee.DeleteAll();
        }

        public EmployeeTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_tests;";
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
      public void Equals_ReturnsTrueIfNamesAreTheSame_Employee()
      {
        // Arrange, Act
        Employee firstEmployee = new Employee("Name");
        Employee secondEmployee = new Employee("Name");

        // Assert
        Assert.AreEqual(firstEmployee.GetName(), secondEmployee.GetName());
      }

      [TestMethod]
      public void Save_SavesToDatabase_EmployeeList()
      {
        //Arrange
        Employee testEmployee = new Employee("Name");

        //Act
        testEmployee.Save();
        List<Employee> result = Employee.GetAll();
        List<Employee> testList = new List<Employee>{testEmployee};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }


        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          Employee testEmployee = new Employee("Name");

          //Act
          testEmployee.Save();
          Employee savedEmployee = Employee.GetAll()[0];

          int result = savedEmployee.GetId();
          int testId = testEmployee.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }



  }
}
