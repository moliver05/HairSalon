using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Test
{
  [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_tests;";
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
      public void Equals_ReturnsTrueIfNamesAreTheSame_Client()
      {
        // Arrange, Act
        Client firstClient = new Client("Name", 1);
        Client secondClient = new Client("Name", 2);

        // Assert
        Assert.AreEqual(firstClient, secondClient);
      }

      [TestMethod]
      public void Save_SavesToDatabase_ClientList()
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

      [TestMethod]
      public void Save_AssignsIdToObject_Id()
      {
        //Arrange
        Client testClient = new Client("Name", 1);
        testClient.Save();

        //Act
        Client savedClient = Client.GetAll()[0];

        int result = savedClient.GetClientId();
        int testId = testClient.GetClientId();

        //Assert
        Assert.AreEqual(testId, result);
      }

  }
}
