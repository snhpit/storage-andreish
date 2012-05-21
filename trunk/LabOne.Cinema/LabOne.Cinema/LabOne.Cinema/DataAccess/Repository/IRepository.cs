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

        void Remove<T>(T item);

        void Remove<T>(string id) where T : EntityBase;
    }
}