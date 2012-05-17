using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Data"));
            var visitor1 = new Visitor { FirstName = "Andrei", LastName = "Shostik", PasportNumber = 2090186 };
            var visitor2 = new Visitor { FirstName = "Ololo", LastName = "Ololosh", PasportNumber = 777777 };
            //IEnumerable
            List<Visitor> li = new List<Visitor> { visitor1, visitor2 };

            //XmlRepository xmlRepository = new XmlRepository(path);
            //xmlRepository.Save(visitor1, Guid.NewGuid().ToString());
            var v = li.ToArray();
            XmlDatabase xmlDatabase = new XmlDatabase(path);
            var x = xmlDatabase.Write(v, Guid.NewGuid().ToString());
            //Console.WriteLine(x);
            //Console.ReadKey(true);
        }
    }
}