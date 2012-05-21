using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.DataAccess.Repository
{
    public interface IRepository
    {
        T Get<T>(string id);

        IEnumerable<T> GetAll<T>();

        bool Save<T>(T items);

        void Remove<T>(T item);

        void Remove<T>(string id);

        void Update<T>(T item);
    }
}