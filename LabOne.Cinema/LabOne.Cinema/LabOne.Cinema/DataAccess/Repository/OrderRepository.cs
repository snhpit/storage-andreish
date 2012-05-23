using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class OrderRepository : Repository, ICrudRepository<Order>
    {
        private List<Order> _orders;

        public OrderRepository(string path, string fileExtension)
            : base(path, fileExtension)
        {
            _orders = GetAll<Order>().ToList();
        }

        public OrderRepository(DataBase database)
            : base(database)
        {
            _orders = GetAll<Order>().ToList();
        }

        ~OrderRepository()
        {
            try
            {
                SaveAll(_orders);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, your data is not saved.\n{0}\n{1}", e.Message, e.Source);
            }
        }

        public Order Get(string id)
        {
            return _orders.FirstOrDefault(elem => elem.ID == id);
        }

        public void Remove(Order item)
        {
            Remove(item.ID);
        }

        public void Remove(string id)
        {
            _orders.Remove(Get(id));
        }

        public void Update(Order item)
        {
            var order = new Order(item.ID, item.Visitor, item.Cashier, item.Film, item.DateOrder);
            Remove(item);
            _orders.Add(order);
        }

        public void Create(Order item)
        {
            Update(item);
        }
    }
}