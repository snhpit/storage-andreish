using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.DataAccess.Database
{
    interface IDataBase
    {
        bool WriteData<T>(T data);

        IEnumerable<T> ReadFile<T>();
    }
}