using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public T Get<T>(string id) where T : EntityBase
        {
            var data = GetAll<T>();
            return data == null ? null : data.FirstOrDefault(elem => elem.ID == id);
        }

        public void Remove<T>(T item)
        {
            GetAll<T>().ToList().Remove(item);
        }

        public void Remove<T>(string id) where T : EntityBase
        {
            Remove(Get<T>(id));
        }

        public IEnumerable<T> GetAll<T>()
        {
            IEnumerable<T> items;
            try
            {
                items = _dataBase.ReadFile<T>();
            }
            catch (ItemNotFoundException exception)
            {
                Console.WriteLine(string.Format("Item with type {1} not found.\n{0}", exception.Message, typeof(T).Name));
                return null;
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message, exception.Source, exception.FusionLog);
                return null;
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("Message = {0}, paramName = {1}", exception.Message, exception.ParamName);
                return null;
            }
            return items;
        }

        public bool SaveAll<T>(T items)
        {
            try
            {
                if (items.GetType().IsValueType)
                {
                    throw new TypeIsNotEnumerableException(string.Format(
                        "Item is value type {0}. Need to be Enumerable", items.GetType().Name));
                }
                if (typeof(T).Name != typeof(IEnumerable).Name)
                {
                    throw new TypeIsNotEnumerableException(string.Format(
                        "Item is not Enumerable type {0}. Need to be Enumerable", items.GetType().Name));
                }
                _dataBase.WriteData(items);
            }
            catch (TypeIsNotEnumerableException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
            return true;
        }
    }
}