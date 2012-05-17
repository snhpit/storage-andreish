using System;
using System.Collections.Generic;
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
            return _database.ReadFile<TItem>(id);
        }

        public IEnumerable<TItem> GetAll<TItem>()
        {
            return _database.ReadAllFiles<TItem>();
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