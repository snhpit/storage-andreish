using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public class FileRepository<T> where T : ItemBase
    //: IRepository<T> where T : ItemBase
    {
        private const string Managers = "c:\\Managers.txt";

        public T GetItem(int id)
        {
            return GetItems()
                .FirstOrDefault(i => i.id == id);
        }

        public IEnumerable<T> GetItems()
        {
            string[] managers = File.ReadAllLines(Managers);
            return
                managers.Select(c =>
                {
                    string[] manager = c.Split('|');
                    return new typeof(T)(System.Int32.Parse(manager[0]), manager[1]);
                });
        }

        public void Save(T item)
        {
            var itemString = string.Join("|", item.id, item.name) + "\n";
            File.AppendAllText(Managers, itemString);
        }

        public void Remove(T item)
        {
            string[] managers = File.ReadAllLines(Managers);

            IEnumerable<T> SelectedManagers = managers.Select(c =>
            {
                string[] manager = c.Split('|');

                return new T(System.Int32.Parse(manager[0]), manager[1]);
            });

            StreamWriter a = new StreamWriter(Managers, false);
            foreach (var i in SelectedManagers)
            {
                if (i.name != item.name && i.id != item.id)
                {
                    var itemString = string.Join("|", i.id, i.name) + "\n";
                    a.Write(itemString);
                }
            }

            a.Close();
        }

        public void Remove(int id)
        {
            string[] managers = File.ReadAllLines(Managers);

            IEnumerable<T> SelectedManagers = managers.Select(c =>
            {
                string[] manager = c.Split('|');

                return new T(System.Int32.Parse(manager[0]), manager[1]);
            });

            StreamWriter a = new StreamWriter(Managers, false);
            foreach (var i in SelectedManagers)
            {
                if (i.id != id)
                {
                    var itemString = string.Join("|", i.id, i.name) + "\n";
                    a.Write(itemString);
                }
            }
        }

        public void Update(T item)
        {
            IRepository<T> _managerRepository = new ManagerRepository();
            IEnumerable<T> managerRepository = _managerRepository.GetItems();

            T[] manager = managerRepository.ToArray();

            manager[item.id - 1] = item;

            StreamWriter clientRefresh = new StreamWriter(Managers);
            clientRefresh.Flush();
            clientRefresh.Close();

            foreach (var i in manager)
            {
                _managerRepository.Save(i);
            }
        }
    }
}