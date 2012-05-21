using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.DataAccess.Repository
{
    public interface ICrudRepository<T>
    {
        void Create(T item);

        T Get(string id);

        void Remove(T item);

        void Remove(string id);

        void Update(T item);
    }
}