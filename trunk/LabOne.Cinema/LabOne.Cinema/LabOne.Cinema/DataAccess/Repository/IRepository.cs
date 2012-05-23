using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>();

        bool SaveAll<T>(T items);

        T Get<T>(string id) where T : EntityBase;

        bool Remove<T>(T item);

        bool Remove<T>(string id) where T : EntityBase;

        void Create<T>(T item);

        bool Update<T>(T item);
    }
}