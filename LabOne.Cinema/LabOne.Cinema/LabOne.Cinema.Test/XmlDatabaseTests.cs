using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;
using NUnit.Framework;

namespace LabOne.Cinema.Test
{
    [TestFixture]
    public class XmlDatabaseTests
    {
        private string _path;
        private XmlDatabase _db;

        [SetUp]
        public void TestSetUp()
        {
            _db = new XmlDatabase(_path);
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DataOut"));

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
            Visitor visitor = new Visitor { ID = "1", FirstName = "Andrei", LastName = "Shostik" };
            string filename = _db.Write(visitor, visitor.ID);

            Assert.That(filename, Is.StringEnding(@"\Visitor.xml"));

            Assert.IsTrue(File.Exists(filename), "Expect file: " + filename);

            string fileData = File.ReadAllText(filename);
        }
    }
}