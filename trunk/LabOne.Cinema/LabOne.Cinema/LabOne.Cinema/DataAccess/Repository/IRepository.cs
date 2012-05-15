using System.Collections.Generic;

namespace LabOne.Cinema.DataAccess.Repository
{
    public interface IRepository<T>
    {
        T GetItem(string id);

        IEnumerable<T> GetItems();

        void Save(T item);

        void Remove(T item);

        void Remove(string id);

        void Update(T item);
    }
}