using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Test
{
  [TestClass]
    public class SpecialtyTests : IDisposable
    {
        public void Dispose()
        {
            Specialty.DeleteAll();
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
      public void Equals_Overrides_IsTrue()
      {
        // Arrange, Act
        Specialty oneSpecialty = new Specialty("Fade");
        Specialty twoSpecialty = new Specialty("Curls");

        // Assert
        Assert.AreEqual(oneSpecialty.GetSpecialtyName(), twoSpecialty.GetSpecialtyName());
      }
      [TestMethod]
      public void Save_Returns_True()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Mochi");

        //Act
        testSpecialty.Save();
        List<Specialty> result = Specialty.GetAll();
        List<Specialty> testList = new List<Specialty>{testSpecialty};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          Specialty testSpecialty = new Specialty("Name");

          //Act
          testSpecialty.Save();
          Specialty savedSpecialty = Specialty.GetAll()[0];

          int result = savedSpecialty.GetSpecialtyId();
          int testId = testSpecialty.GetSpecialtyId();

          //Assert
          Assert.AreEqual(testId, result);
        }

  }
}
