using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Repository;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.BusinessLogic
{
    public class CinemaLogic
    {
        private readonly IRepository _repository;
        private ICrudRepository<EntityBase> _crudRepository;

        public CinemaLogic(IRepository repository, ICrudRepository<EntityBase> crudRepository)
        {
            _repository = repository;
            _crudRepository = crudRepository;
        }

        public CinemaLogic(IRepository repository)
        {
            _repository = repository;
        }

        public bool CanBuyTicket()
        {
            return _repository.GetAll<Seat>().Select(elem => elem.SeatNumber).Count() <= Seat.MaxSeats;
        }

        public void BuyTicket(Film film, Visitor visitor, Cashier cashier, int seat)
        {
            var dateTime = new DateTime();
            _repository.Create(new Order(visitor, cashier, film, dateTime));
            var order = _repository.Get<Order>(GetAllId<Order>().Last());
            _repository.Create(new Seat(order, seat));
        }

        public void CreateVisitor(Visitor item)
        {
            _repository.Create(item);
        }

        public Film GetFilmByTitle(string filmTitle)
        {
            return _repository.GetAll<Film>().FirstOrDefault(elem => elem.Title == filmTitle);
        }

        public IEnumerable<string> GetAllCashiers()
        {
            return _repository.GetAll<Cashier>()
                .Select(elem => string.Format("{0} {1}", elem.FirstName, elem.LastName));
        }

        public Cashier GetCashierByLastName(string lastName)
        {
            return _repository.GetAll<Cashier>().FirstOrDefault(elem => elem.LastName == lastName);
        }

        public IEnumerable<string> GetAllFilms()
        {
            return _repository.GetAll<Film>()
                .Select(elem => string.Format("{0} {1} {2}", elem.Title, elem.Year, elem.Genre));
        }

        public IEnumerable<int> GetAllSeats()
        {
            return _repository.GetAll<Seat>().Select(elem => elem.SeatNumber);
        }

        //public IEnumerable<Film> GetFilmByDateOrder(DateTime dateTime)
        //{
        //    return GetBaseInfoAboutType<Order>().SelectMany(elem => elem);
        //}

        public IEnumerable<string> GetAllId<T>() where T : EntityBase
        {
            return _repository.GetAll<T>().Select(elem => elem.ID);
        }

        public IEnumerable<List<string>> GetBaseInfoAboutType<T>()
        {
            return _repository.GetAll<T>().Select(elem => elem.ToString().Split('|').ToList());
        }

        public IEnumerable<int> GetNumbersOfSeatsByOrderId(string id)
        {
            return GetBaseInfoAboutType<Seat>().Where(elem => elem[0] == id)
                .Select(elem => int.Parse(elem[1]));
        }

        public void BookTicket()
        {
            throw new NotImplementedException();
        }
    }
}