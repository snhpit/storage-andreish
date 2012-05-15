using System;
using System.IO;
using LabOne.Cinema.DataAccess;
using LabOne.Cinema.Tests.Models;
using NUnit.Framework;

namespace LabOne.Cinema.Tests
{
    [TestFixture]
    public class XmlDbLoadTests
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
            _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\SampleData"));
        }

        [Test]
        public void test()
        {
            Visitor person101 = _db.Read<Visitor>("101");

            Assert.That(person101, Is.Not.Null);
            Assert.That(person101.VisitorId, Is.EqualTo("101"));
            Assert.That(person101.Name, Is.EqualTo("Jack"));
            Assert.That(person101.DateOfBirth, Is.EqualTo(new DateTime(1968, 3, 7)));
        }

        [Test]
        public void Read_all_rows_of_Person()
        {
            var rows = _db.Read<Visitor>();

            Assert.That(rows.Length, Is.EqualTo(2));
            Assert.That(rows[0].VisitorId, Is.EqualTo("101"));
            Assert.That(rows[0].Name, Is.EqualTo("Jack"));
            Assert.That(rows[1].VisitorId, Is.EqualTo("102"));
            Assert.That(rows[1].Name, Is.EqualTo("Jill"));
        }

        [Test]
        public void Read_in_a_Pet()
        {
            Cashier cashier = _db.Read<Cashier>("bad70af3-7145-44c0-bb4d-0aae048a5628");

            Assert.That(cashier, Is.Not.Null);
            Assert.That(cashier.ID, Is.EqualTo("bad70af3-7145-44c0-bb4d-0aae048a5628"));
            Assert.That(cashier.Name, Is.EqualTo("Billy"));
        }

        [Test]
        public void There_is_only_1_dog()
        {
            var rows = _db.Read<Cashier>();
            Assert.That(rows.Length, Is.EqualTo(1));
        }
    }
}