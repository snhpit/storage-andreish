using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly DataBase _dataBase;
        //private ExpandoObject _typeGenerator;

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

        public bool Remove<T>(T item)
        {
            var itemList = GetAll<T>().ToList();
            if (itemList.Count == 0) return false;
            if (itemList.Find(elem => (dynamic)elem == item) != null)
            {
                itemList.Remove(item);
                SaveAll(itemList);
                return true;
            }
            return false;
        }

        public bool Remove<T>(string id) where T : EntityBase
        {
            return Remove(Get<T>(id));
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
                if (typeof(T).Namespace != typeof(IEnumerable).Namespace && typeof(T).Namespace != typeof(IEnumerable<T>).Namespace)
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

        public virtual void Create<T>(T item)
        {
            Update(item);
        }

        public bool Update<T>(T item)
        {
            var instance = TypeGenerator.GenerateType(item);
            var allItems = GetAll<T>().ToList();
            allItems.Remove(item);
            allItems.Add(instance);
            return SaveAll(allItems);
        }
    }
}