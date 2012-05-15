using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class XmlRepository
    {
        protected readonly XmlDatabase _database;

        public XmlRepository(string path)
        {
            _database = new XmlDatabase(path);
        }

        public TItem Get<TItem>(string id)
        {
            return _database.Read<TItem>(id);
        }

        public TItem[] GetAll<TItem>()
        {
            return _database.Read<TItem>();
        }

        public void Save<TItem>(TItem item, string id)
        {
            _database.Write(item, id);
        }

        public void Delete<T>(string id)
        {
            _database.Delete<T>(id);
        }

        public void Delete(Type type, string id)
        {
            _database.Delete(type, id);
        }
    }
}