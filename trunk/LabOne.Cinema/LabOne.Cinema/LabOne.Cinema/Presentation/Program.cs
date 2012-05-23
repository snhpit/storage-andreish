using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using LabOne.Cinema.BusinessLogic;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.Presentation
{
    public class Program
    {
        private static CinemaConsole _cinemaConsole;
        private static CinemaLogic _cinemaLogic;
        private static Repository _repository;

        private static void Init()
        {
            _repository = new Repository(new XmlDataBase(@"Data\XmlDB\"));
            _cinemaLogic = new CinemaLogic(_repository);
            _cinemaConsole = new CinemaConsole(_cinemaLogic);
        }

        public static void Main(string[] args)
        {
            Init();
            if (_cinemaLogic.GetAllCashiers() == null)
            {
                new Initializer(_repository);
            }

            _cinemaConsole.Menu();
        }
    }
}