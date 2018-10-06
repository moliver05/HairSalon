using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Employee.DeleteAll();
      Specialty.DeleteAll();
    }


    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }


    [TestMethod]
      public void Save_ToDatabase_ClientList()
      {
        //Arrange
        Client testClient = new Client("Name", 1);
        testClient.Save();

        //Act
        List<Client> result = Client.GetAll();
        List<Client> testList = new List<Client>{testClient};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

  }
}
