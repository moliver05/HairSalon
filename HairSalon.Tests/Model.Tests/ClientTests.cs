using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;

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
            Stylist.DeleteAll();
        }
        [TestMethod]
        public void GetAll_Returns_True ()
        {
            List<Client> allClients = Client.GetAll();
            //
            Assert.AreEqual(allClients.Count);
        }

        [TestMethod]
        public void BothClients_Returns_True()
        {
            Client clientOne = new Client("John", 1);
            Client clientTwo = new Client("John", 2);
            //
            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void SaveMethod_Returns_True()
        {
            Client testClient = new Client("Jack");
            testClient.Save();
            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client> { testClient };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_Returns_True()
        {
            Client testClient = new Client("Mark");
            testClient.Save();
            //
            Client result = Client.Find(testClient.GetId());
            Assert.AreEqual(testClient, result);
        }

        [TestMethod]
        public void Update_ChangesClientNameInDatabase_Client()
        {
            Client firstClient = new Client("Jennifer", 1);
            firstClient.Save();
            Client newStylist = new Client("John", 1, firstClient.GetId());
            firstClient.Update("John", 1);
            Client result = Client.Find(firstClient.GetId());
            Assert.AreEqual(newStylist, result);
        }

    }
  }
