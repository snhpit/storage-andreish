using System;
using System.Collections.Generic;
using System.Linq;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly IDataBase _dataBase;

        public Repository(string path, string fileExtension)
        {
            try
            {
                _dataBase = SelectDataBase(path, fileExtension);
            }
            catch (DataBaseNotFoundException exeption)
            {
                Console.WriteLine(exeption.Message, exeption.Source);
            }
        }

        public Repository(DataBase database)
        {
            _dataBase = database;
        }

        private DataBase SelectDataBase(string path, string fileExtension)
        {
            var dataBase = CreateDataBase();
            if (!dataBase.ContainsKey(fileExtension))
            {
                throw new DataBaseNotFoundException(fileExtension);
            }
            return dataBase[fileExtension](path);
        }

        private Dictionary<string, Func<string, DataBase>> CreateDataBase()
        {
            return new Dictionary
            <string, Func<string, DataBase>>
                {
                    { "xml", path => new XmlDataBase(path) },
                    { "txt", path => new FileDataBase(path) }
                };
        }

        //public Person[] GetAllPeople()
        //{
        //    return GetAll<Person>();
        //}

        //public Unit[] GetAllUnits()
        //{
        //    return GetAll<Unit>();
        //}

        //public Person GetPerson(string id)
        //{
        //    return Get<Person>(id);
        //}

        //public Unit GetUnit(string id)
        //{
        //    return Get<Unit>(id);
        //}

        //public void Save<TItem>(TItem item) where TItem : ModelBase
        //{
        //    Save(item, item.ID);
        //}

        public T Get<T>(string id)
        {
            return GetAll<T>().FirstOrDefault(elem => ((dynamic)elem).ID == id);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _dataBase.ReadFile<T>();
        }

        public bool Save<T>(T items)
        {
            try
            {
                _dataBase.WriteData(items);
            }
            catch (IsValueTypeException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
            return true;
        }

        public void Remove<T>(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove<T>(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}