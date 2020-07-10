using NUnit.Framework;
using NSubstitute;
using ContactManager.Contracts;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Services;
using System.IO;

namespace Tests
{
    public class Tests
    {
        IFileService fileMock;
        List<Contact> contactsList;
        Contact contact1;

        [SetUp]
        public void Setup()
        {
            fileMock = Substitute.For<IFileService>();
            contact1 = new Contact()
            {
                Address = "addr1",
                FirstName = "name1",
                LastName = "surr1",
                Phones = new List<string>() { "123", "321" }
            };
            contactsList = new List<Contact>()
            {
                contact1
            };
        }

        [Test]
        public void TestGetAll()
        {
            fileMock.ReadFromFile().Returns(contactsList);

            var service = new ContactService()
            {
                FileService = fileMock
            };

            var result = service.GetAllContacts();

            Assert.AreEqual(contactsList.Count, result.Count);
            Assert.AreEqual(contactsList[0].Address, result[0].Address);

            fileMock.Received().ReadFromFile();
        }

        [Test]
        public void TestCreateNew()
        {
            fileMock.WriteToFile(Arg.Any<List<Contact>>());

            var service = new ContactService()
            {
                FileService = fileMock
            };
            var result = service.CreateContact(contact1);

            Assert.AreEqual(contact1.Address, result.Address);

            fileMock.Received().WriteToFile(Arg.Any<List<Contact>>());
        }

        //other methods are the same when mocking
        //also all methods tested using UI

    }


}