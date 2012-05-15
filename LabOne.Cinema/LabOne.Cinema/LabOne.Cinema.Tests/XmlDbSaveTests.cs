using System;
using System.IO;
using LabOne.Cinema;
using LabOne.Cinema.DataAccess;
using LabOne.Cinema.Tests.Models;
using NUnit.Framework;

namespace LabOne.Cinema.Tests
{
    [TestFixture]
    public class XmlDbSaveTests
    {
        private string _path;
        private XmlDatabase _db;

        [SetUp]
        public void TestSetUp()
        {
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DataOut"));
            _db = new XmlDatabase(_path);
            string[] files = Directory.GetFiles(_path, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }

        [Test]
        public void SaveVisitorTest()
        {
            Visitor visitor = new Visitor { VisitorId = "1", Name = "Bob", DateOfBirth = new DateTime(1968, 3, 7) };
            string filename = _db.Write(visitor, visitor.VisitorId);

            Assert.That(filename, Text.EndsWith(@"\Visitor-1.xml"));
            Assert.IsTrue(File.Exists(filename), "Expect file: " + filename);

            string fileData = File.ReadAllText(filename);
            Assert.That(fileData, Text.Contains("\"VisitorId\": \"1\""));
            Assert.That(fileData, Text.Contains("\"Name\": \"Bob\""));
        }

        [Test]
        public void SaveCashierTest()
        {
            Cashier dog = new Cashier { ID = _db.CreateID(), Name = "Charlie" };
            string filename = _db.Write(dog, dog.ID);

            Assert.That(filename, Text.Contains(@"\Cashier-"));
            Assert.IsTrue(File.Exists(filename), "Expect file: " + filename);

            string fileData = File.ReadAllText(filename);
            Assert.That(fileData, Text.Contains("\"Name\": \"Charlie\""));
        }

        [Test]
        public void CreateIdTest()
        {
            string id = _db.CreateID();
            Assert.That(id, Is.Not.Null);
        }
    }
}