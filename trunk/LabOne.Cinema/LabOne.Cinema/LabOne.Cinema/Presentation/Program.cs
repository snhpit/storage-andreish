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

            var repoXml = new Repository(xml);
            var repoFile = new Repository(file);
            // var r = repoXml.SaveAll(t);
            //new Initializer(repoXml);

            var w = repoXml.Get<Visitor>("f8bdb86e-5087-4cfa-867a-d2a94e03290e");

            var visRep = new VisitorRepository(xml);
            var x = visRep.Get("f8bdb86e-5087-4cfa-867a-d2a94e03290e");
            //visRep.;

            var seatFile = new SeatRepository(file);
            var qx = seatFile.GetNumbersOfSeatsByOrderId("f8bdb86e-5087-4cfa-867a-d2a94e03290e");


            //repo1.
            Console.ReadKey(true);
        }
    }
}