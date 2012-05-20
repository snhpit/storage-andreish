using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            var initializer = new Initializer();
            initializer.GoInit();

            var prepository = new Repository<>()

            Console.ReadKey(true);
        }
    }
}