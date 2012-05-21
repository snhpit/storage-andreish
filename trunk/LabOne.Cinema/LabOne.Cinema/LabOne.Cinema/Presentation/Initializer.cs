using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.Presentation
{
    public class Initializer
    {
        const string xmlPath = @"Data\XmlDB\";
        const string txtPath = @"Data\TxtDB\";

        private DataBase _database;
        //private Repository _repository;

        public Initializer(string path, string fileExtension)
        {
            //_database.ReadFile<Visitor>(); why is it possible?
            if (fileExtension == "xml")
            {
                _database = new XmlDataBase(path);
            }
            if (fileExtension == "txt")
            {
                _database = new FileDataBase(path);
            }
        }

        public Initializer(Repository repository)
        {
            InitData().All(repository.SaveAll);
        }

        public bool GoInit()
        {
            return _database != null && InitData().All(_database.WriteData);
        }

        private static IEnumerable<IEnumerable> InitData()
        {
            var listVisitors = new List<Visitor>
                {
                    new Visitor {FirstName = "Andrei", LastName = "Andreev", PasportNumber = 111111},
                    new Visitor {FirstName = "Oleg", LastName = "Olegov", PasportNumber = 222222},
                    new Visitor {FirstName = "Roman", LastName = "Romanov", PasportNumber = 333333},
                    new Visitor {FirstName = "Igor", LastName = "Igorev", PasportNumber = 444444},
                    new Visitor {FirstName = "Goga", LastName = "Gogov", PasportNumber = 777777},
                    new Visitor {FirstName = "Stepan", LastName = "Stepanov", PasportNumber = 444444},
                };

            var listCashiers = new List<Cashier>
                {
                    new Cashier {FirstName = "Olga", LastName = "Drug"},
                    new Cashier {FirstName = "Yulia", LastName = "Pavlova"}
                };

            var listFilms = new List<Film>
                {
                    new Film {Title = "HI!", Year = 2010, Genre = "Comedy"},
                    new Film {Title = "777", Year = 2010, Genre = "Action"},
                    new Film {Title = "The world is mine", Year = 1999, Genre = "Drama"},
                    new Film {Title = "Brlar baarhgl... and all about zombies", Year = 2012, Genre = "Horror"},
                    new Film {Title = "Wow! Black king", Year = 2012, Genre = "Comedy"},
                    new Film {Title = "War is war", Year = 2011, Genre = "War"},
                    new Film {Title = "HI! 2", Year = 2011, Genre = "Comedy"},
                    new Film {Title = "HI! 3", Year = 2012, Genre = "Comedy"},
                    new Film {Title = "30000000", Year = 2006, Genre = "Historical"},
                    new Film {Title = "Last warrior", Year = 2012, Genre = "Fantasy"},
                };

            var listOrders = new List<Order>
                {
                    new Order
                        {
                            Cashier = listCashiers[0],
                            Visitor = listVisitors[0],
                            DateOrder = new DateTime(),
                            Film = listFilms[9]
                        }
                };

            var listSeats = new List<Seat>
                {
                    new Seat {Order = listOrders[0], SeatNumber = 1},
                    new Seat {Order = listOrders[0], SeatNumber = 2},
                    new Seat {Order = listOrders[0], SeatNumber = 3},
                };

            return new List<IEnumerable> { listVisitors, listCashiers, listFilms, listOrders, listSeats };
        }
    }
}