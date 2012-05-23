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
    public class DatabaseTests
    {
        private string _path;
        private Repository _xmlRepository;
        private Repository _fileRepository;
        private XmlDataBase _xmlDb;
        private FileDataBase _fileDb;
        private string _fileName;
        private bool _fileIsWritten;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\DataOut"));

            _xmlDb = new XmlDataBase(_path);
            _fileDb = new FileDataBase(_path);

            _xmlRepository = new Repository(_xmlDb);
            _fileRepository = new Repository(_fileDb);

            // steb by step | write file -> read file -> delete file
        }

        [SetUp]
        public void TestSetUp()
        {
        }

        [Test]
        public void XmlWriteVisitorsTest()
        {
            Visitor visitor1 = new Visitor("1", "A", "A", 1111);
            Visitor visitor2 = new Visitor("2", "B", "B", 1112);
            Visitor visitor3 = new Visitor("3", "C", "C", 1113);
            _fileIsWritten = _xmlRepository.SaveAll(new List<Visitor> { visitor1, visitor2, visitor3 }.AsEnumerable());

            Assert.IsTrue(_fileIsWritten);
            //Assert.That(_filename, Is.StringEnding(@"\Visitors.xml"));
            //Assert.IsTrue(File.Exists(_filename), "Expect file: " + _filename);
        }

        [Test]
        public void XmlReadVisitorsTest()
        {
            var visitor = _xmlRepository.GetAll<Visitor>().ToArray();

            Assert.That(visitor[0], Is.Not.Null);
            Assert.That(visitor[0].ID, Is.EqualTo("1"));
            Assert.That(visitor[0].FirstName, Is.EqualTo("A"));
            Assert.That(visitor[0].LastName, Is.EqualTo("A"));
            Assert.That(visitor[0].PasportNumber, Is.EqualTo(1111));

            Assert.That(visitor[1], Is.Not.Null);
            Assert.That(visitor[1].ID, Is.EqualTo("2"));
            Assert.That(visitor[1].FirstName, Is.EqualTo("B"));
            Assert.That(visitor[1].LastName, Is.EqualTo("B"));
            Assert.That(visitor[1].PasportNumber, Is.EqualTo(1112));
        }

        [Test]
        public void XmlDeleteVisitorsTest()
        {
            var isDeleted = _xmlDb.Delete<Cashier>();
            string[] files = Directory.GetFiles(_path, "*.*", SearchOption.TopDirectoryOnly);

            Assert.That(files.FirstOrDefault(file => _fileName != null && file == _fileName) == null);
        }

        [Test]
        public void FileWriteVisitorsTest()
        {
            Visitor visitor1 = new Visitor("1", "A", "A", 1111);
            Visitor visitor2 = new Visitor("2", "B", "B", 1112);
            Visitor visitor3 = new Visitor("3", "C", "C", 1113);
            _fileIsWritten = _fileRepository.SaveAll(new List<Visitor> { visitor1, visitor2, visitor3 }.AsEnumerable());

            Assert.IsTrue(_fileIsWritten);
            //Assert.That(_fileName, Is.StringEnding(@"\Visitors.txt"));
            //Assert.IsTrue(File.Exists(_fileName), "Expect file: " + _fileName);
        }

        [Test]
        public void FileReadVisitorsTest()
        {
            var visitor = _fileRepository.GetAll<Visitor>().ToArray();

            Assert.That(visitor[0], Is.Not.Null);
            Assert.That(visitor[0].ID, Is.EqualTo("1"));
            Assert.That(visitor[0].FirstName, Is.EqualTo("A"));
            Assert.That(visitor[0].LastName, Is.EqualTo("A"));
            Assert.That(visitor[0].PasportNumber, Is.EqualTo(1111));

            Assert.That(visitor[1], Is.Not.Null);
            Assert.That(visitor[1].ID, Is.EqualTo("2"));
            Assert.That(visitor[1].FirstName, Is.EqualTo("B"));
            Assert.That(visitor[1].LastName, Is.EqualTo("B"));
            Assert.That(visitor[1].PasportNumber, Is.EqualTo(1112));
        }

        [Test]
        public void FileDeleteVisitorsTest()
        {
            var isDeleted = _fileDb.Delete<Cashier>();
            string[] files = Directory.GetFiles(_path, "*.*", SearchOption.TopDirectoryOnly);

            Assert.That(files.FirstOrDefault(file => _fileName != null && file == _fileName) == null);
        }
    }
}