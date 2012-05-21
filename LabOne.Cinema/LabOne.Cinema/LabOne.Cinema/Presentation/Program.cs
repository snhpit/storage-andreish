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
            repo1.Save(new Visitor());
            new Initializer(repo1);
            var order = repo1.Get<Order>("f97dab5a-1b83-4385-8426-2e19e335f08f");

            Console.ReadKey(true);
        }
    }
}