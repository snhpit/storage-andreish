using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class CashierRepository : Repository, ICrudRepository<Cashier>
    {
        private List<Cashier> _cashiers;

        public CashierRepository(string path, string fileExtension)
            : base(path, fileExtension)
        {
            _cashiers = GetAll<Cashier>().ToList();
        }

        public CashierRepository(DataBase database)
            : base(database)
        {
            _cashiers = GetAll<Cashier>().ToList();
        }

        ~CashierRepository()
        {
            try
            {
                SaveAll(_cashiers);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, your data is not saved.\n{0}\n{1}", e.Message, e.Source);
            }
        }

        public Cashier Get(string id)
        {
            return _cashiers.FirstOrDefault(elem => elem.ID == id);
        }

        public void Remove(Cashier item)
        {
            Remove(item.ID);
        }

        public void Remove(string id)
        {
            _cashiers.Remove(Get(id));
        }

        public void Update(Cashier item)
        {
            var cashier = new Cashier
            {
                ID = item.ID,
                FirstName = item.FirstName,
                LastName = item.LastName,
            };
            Remove(item);
            _cashiers.Add(cashier);
        }

        public void Create(Cashier item)
        {
            Update(item);
        }
    }
}