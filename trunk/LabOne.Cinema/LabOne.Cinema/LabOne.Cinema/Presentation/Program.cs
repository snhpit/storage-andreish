using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var initializer1 = new Initializer(@"Data\XmlDB\", "xml");
            //initializer1.GoInit();

            //var repository = new Repository(@"Data\XmlDB\", "xml");
            ////repository.Save();

            var xml = new XmlDataBase(@"Data\XmlDB\");
            var file = new FileDataBase(@"Data\TxtDB\");

            var t = new List<Visitor> { new Visitor() };

            var repo1 = new Repository(xml);
            var repo2 = new Repository(file);
            // var r = repo1.SaveAll(t);
            //new Initializer(repo1);

            var w = repo1.Get<Visitor>("cfbb0795-baee-4d99-89d8-6d0995df54c9");

            var ww = new VisitorRepository(xml);
            var x = ww.Get("cfbb0795-baee-4d99-89d8-6d0995df54c9");

            var rr = new SeatRepository(xml);
            var qx = rr.GetNumbersOfSeats("f55fc07a-4bb0-440b-bd2e-6fd6bf5857ca");

            //repo1.
            Console.ReadKey(true);
        }
    }
}