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
                    new Visitor ("Andrei", "Andreev", 111111),
                    new Visitor ( "Oleg", "Olegov", 222222),
                    new Visitor ("Roman",  "Romanov",  333333),
                    new Visitor ( "Igor", "Igorev", 444444),
                    new Visitor("Goga", "Gogov",  777777),
                    new Visitor ("Stepan", "Stepanov",444444),
                };

            var listCashiers = new List<Cashier>
                {
                    new Cashier ( "Olga", "Drug"),
                    new Cashier ( "Yulia", "Pavlova")
                };

            var listFilms = new List<Film>
                {
                    new Film ( "HI!", "Comedy", 2010),
                    new Film ( "777",   "Action",2010),
                    new Film ( "The world is mine",  "Drama", 1999),
                    new Film ( "Brlar baarhgl... and all about zombies", "Horror", 2012),
                    new Film ("Wow! Black king",  "Comedy", 2012),
                    new Film ( "War is war", "War",  2011),
                    new Film ( "HI! 2",  "Comedy", 2011),
                    new Film ( "HI! 3","Comedy", 2012),
                    new Film ( "30000000", "Historical",  2006),
                    new Film ( "Last warrior", "Fantasy", 2012),
                };

            var listOrders = new List<Order>
                                 {
                                     new Order(listVisitors[0],listCashiers[0],listFilms[9],new DateTime()),
                                     new Order (listVisitors[1], listCashiers[0], listFilms[7], new DateTime() )
                                 };

            var listSeats = new List<Seat>
                                {
                                    new Seat ( listOrders[0], 1),
                                    new Seat ( listOrders[0],  2),
                                    new Seat (listOrders[0],  3),
                                    new Seat( listOrders[1], 5),
                                    new Seat ( listOrders[1], 17),
                                    new Seat (listOrders[1],  4)
                                };

            return new List<IEnumerable> { listVisitors, listCashiers, listFilms, listOrders, listSeats };
        }
    }
}