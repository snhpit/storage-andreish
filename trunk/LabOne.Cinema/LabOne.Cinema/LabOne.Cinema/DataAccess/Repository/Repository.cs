using System;
using System.Collections.Generic;
using LabOne.Cinema.DataAccess.Database;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class Repository<T>
    {
        //private Dictionary<string, Func<string, string, Database.Database>> _dictionary;
        private DataBase _database;

        public Repository(string path, string fileExtension)
        {
            _database = SelectDataBase()[fileExtension](path, fileExtension);
        }

        protected DataBase

        protected Dictionary<string, Func<string, string, DataBase>> SelectDataBase()
        {
            return new Dictionary
            <string, Func<string, string, DataBase>>
                {
                    { "xml", (path, fileExtension) => new XmlDataBase(path, fileExtension) },
                    { "txt", (path, fileExtension) => new FileDataBase(path, fileExtension) }
                };
        }

        public Person[] GetAllPeople()
        {
            return GetAll<Person>();
        }

        public Unit[] GetAllUnits()
        {
            return GetAll<Unit>();
        }

        public Person GetPerson(string id)
        {
            return Get<Person>(id);
        }

        public Unit GetUnit(string id)
        {
            return Get<Unit>(id);
        }

        public void Save<TItem>(TItem item) where TItem : ModelBase
        {
            Save(item, item.ID);
        }

        public TItem Create<TItem>() where TItem : ModelBase, new()
        {
            ModelBase item = new TItem();
            item.ID = _database.CreateID();
            return (TItem)item;
        }

        public T Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Save(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}