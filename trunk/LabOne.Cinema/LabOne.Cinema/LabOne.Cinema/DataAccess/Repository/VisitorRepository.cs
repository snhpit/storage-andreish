using System;
using System.Collections.Generic;
using System.Linq;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class VisitorRepository : Repository, ICrudRepository<Visitor>
    {
        private List<Visitor> _visitors;

        public VisitorRepository(string path, string fileExtension)
            : base(path, fileExtension)
        {
            _visitors = GetAll<Visitor>().ToList();
        }

        public VisitorRepository(DataBase database)
            : base(database)
        {
            _visitors = GetAll<Visitor>().ToList();
        }

        ~VisitorRepository()
        {
            try
            {
                SaveAll(_visitors);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, your data is not saved.\n{0}\n{1}", e.Message, e.Source);
            }
        }

        public Visitor Get(string id)
        {
            return _visitors.FirstOrDefault(elem => elem.ID == id);
        }

        public void Remove(Visitor item)
        {
            Remove(item.ID);
        }

        public void Remove(string id)
        {
            _visitors.Remove(Get(id));
        }

        public void Update(Visitor item)
        {
            var visitor = new Visitor
            {
                ID = item.ID,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PasportNumber = item.PasportNumber
            };
            Remove(item);
            _visitors.Add(visitor);
        }

        public void Create(Visitor item)
        {
            Update(item);
        }
    }
}