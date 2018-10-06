using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HairSalon.Test
{
  [TestClass]
    public class SpecialtyTests : IDisposable
    {
        public void Dispose()
        {
            Specialty.DeleteAll();
            Employee.DeleteAll();
        }
        public SpecialtyTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_test;";
        }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act
        int result = Specialty.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void Save_ToDatabase_SpecialtyList()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Name", 1);
        testSpecialty.Save();

        //Act
        List<Specialty> result = Specialty.GetAll();
        List<Specialty> testList = new List<Specialty>{testSpecialty};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }
  }
}
