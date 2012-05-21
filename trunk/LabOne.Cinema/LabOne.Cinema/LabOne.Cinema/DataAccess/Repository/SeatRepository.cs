using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class SeatRepository : Repository
    {
        private List<Seat> _seats;
        private OrderRepository _orderRepository;

        public SeatRepository(string path, string fileExtension)
            : base(path, fileExtension)
        {
            _seats = GetAll<Seat>().ToList();
            _orderRepository = new OrderRepository(path, fileExtension);
        }

        public SeatRepository(DataBase database)
            : base(database)
        {
            _seats = GetAll<Seat>().ToList();
            _orderRepository = new OrderRepository(database);
        }

        ~SeatRepository()
        {
            try
            {
                SaveAll(_seats);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, your data is not saved.\n{0}\n{1}", e.Message, e.Source);
            }
        }


        public IEnumerable<int> GetNumbersOfSeatsByOrderId(string id)
        {
            return
                _seats.Select(elem => elem.ToString().Split('|')).Where(elem => elem[0] == id).Select(
                    elem => int.Parse(elem[1]));
        }
    }
}