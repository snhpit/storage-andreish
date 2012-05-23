using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LabOne.Cinema.BusinessLogic;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.Presentation
{
    public class CinemaConsole
    {
        private readonly CinemaLogic _cinema;

        public CinemaConsole(CinemaLogic cinema)
        {
            _cinema = cinema;
        }

        public void Menu()
        {
            var cashier = LogCashier();
            var exit = false;

            do
            {
                Console.ReadKey(true);
                Console.Clear();
                WriteMenu();
                var operationInt = ChoiseOperation();

                switch (operationInt)
                {
                    case 1:
                        WriteFilms();
                        break;
                    case 2:
                        WriteSeats();
                        break;
                    case 3:
                        MakeOrder(cashier);
                        break;
                    case 4:
                        exit = ExitSession();
                        break;
                }
            }
            while (!exit);
        }

        private void MakeOrder(Cashier cashier)
        {
            Console.WriteLine("Select operation");
            Console.WriteLine("1 - Buy / 2 - Book (not implemented)");
            var operationNumber = ChoiseOperation();
            switch (operationNumber)
            {
                case 1:
                    MakeBuyOrder(cashier);
                    break;
                case 2:
                    try
                    {
                        _cinema.BookTicket();
                    }
                    catch (NotImplementedException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
        }

        private void MakeBuyOrder(Cashier cashier)
        {
            var film = SelectFilms();
            if (film == null)
            {
                return;
            }
            var visitor = NewVisitor();
            var seat = ChooseSeat();
            if (seat == 0)
            {
                return;
            }
            // ToDo: more seat to choose for one order;

            _cinema.BuyTicket(film, visitor, cashier, seat);
        }

        private int ChooseSeat()
        {
            Console.Clear();
            Console.WriteLine("Select seat from 1 to 80");
            var seat = ChoiseOperation();
            if (seat == 0)
            {
                ChooseSeat();
            }
            var seats = _cinema.GetAllSeats();
            if (seats.Any(elem => elem == seat))
            {
                Console.WriteLine("Sorry, this seat is already taken");
                ChooseSeat();
            }
            if (seats.Count() == Seat.MaxSeats)
            {
                return seat;
            }
            return default(int);
        }

        private Visitor NewVisitor()
        {
            Console.Clear();
            Console.WriteLine("Enter your Name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter your LastName");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter your pasport number");
            var pasportNumber = ChoiseOperation();
            if (pasportNumber == 0)
            {
                Console.WriteLine("Please, only digits");
                NewVisitor();
            }
            var visitor = new Visitor(firstName, lastName, pasportNumber);
            _cinema.CreateVisitor(visitor);
            return visitor;
        }

        private Film SelectFilms()
        {
            Console.Clear();
            var films = _cinema.GetAllFilms();
            foreach (var film in films)
            {
                Console.WriteLine(film);
            }
            Console.WriteLine("\nEnter the film title");
            var filmTitle = Console.ReadLine();

            var filmByTitle = _cinema.GetFilmByTitle(filmTitle);

            if (filmByTitle == null)
            {
                Console.WriteLine("Sorry, we can't find this film");
                return null;
            }
            return filmByTitle;
        }

        private int ChoiseOperation()
        {
            var operationString = Console.ReadLine();
            int operationInt;
            int.TryParse(operationString, out operationInt);
            return operationInt;
        }

        private bool ExitSession()
        {
            Console.WriteLine("Do you want to exit?");
            Console.WriteLine("1 - Yes / 0 - No");
            return ChoiseOperation() != 0;
        }

        private void WriteSeats()
        {
            var seats = _cinema.GetAllSeats();
            foreach (var seat in seats)
            {
                Console.Write("{0} ", seat);
            }
        }

        private void WriteFilms()
        {
            Console.Clear();
            var films = _cinema.GetAllFilms();
            foreach (var film in films)
            {
                Console.WriteLine(film);
            }
        }

        private void WriteMenu()
        {
            //Console.Clear();
            // listen hardcore, do hardcode ToDo: change this
            Console.WriteLine("Menu");
            Console.WriteLine("1 - Available movies");
            Console.WriteLine("2 - Busy seats");
            Console.WriteLine("3 - New order / buy ticket");
            Console.WriteLine("4 - Exit");
            Console.Write("Select action: ");
        }

        private Cashier LogCashier()
        {
            Console.Clear();
            Console.WriteLine("Log in. Please, enter your last name.");
            var allCashiers = _cinema.GetAllCashiers();

            foreach (var nextCashier in allCashiers)
            {
                Console.WriteLine(nextCashier);
            }
            var lastName = Console.ReadLine();

            var cashier = _cinema.GetCashierByLastName(lastName);

            if (cashier == null)
            {
                LogCashier();
            }
            Console.WriteLine("Good morning, {0} {1}", cashier.FirstName, cashier.LastName);
            return cashier;
        }
    }
}