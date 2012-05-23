using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using LabOne.Cinema.DataAccess.Database;
using LabOne.Cinema.Entities;

namespace LabOne.Cinema.DataAccess.Repository
{
    public static class TypeGenerator
    {
        public static T GenerateType<T>(T item)
        {
            return MakeType<T>()[typeof(T)](item);
        }

        private static Dictionary<Type, Func<T, T>> MakeType<T>()
        {
            return new Dictionary<Type, Func<T, T>>
                {
                    { typeof(Film), new Func<T, T>(elem => (T)Activator.CreateInstance(typeof(T), new object[] { ((dynamic)elem).ID, ((dynamic)elem).Title, ((dynamic)elem).Genre, ((dynamic)elem).Year })) },
                    { typeof(Cashier), new Func<T, T>(elem => (T)Activator.CreateInstance(typeof(T), new object[] { ((dynamic)elem).ID, ((dynamic)elem).FirstName, ((dynamic)elem).LastName })) },
                    { typeof(Visitor), new Func<T, T>(elem => (T)Activator.CreateInstance(typeof(T), new object[] { ((dynamic)elem).ID, ((dynamic)elem).FirstName, ((dynamic)elem).LastName, ((dynamic)elem).PasportNumber }))},
                    { typeof(Order), new Func<T, T>(elem => (T)Activator.CreateInstance(typeof(T), new object[] { ((dynamic)elem).Visitor, ((dynamic)elem).Cashier, ((dynamic)elem).Film, ((dynamic)elem).DateOrder }))},
                    { typeof(Seat), new Func<T, T>(elem => (T)Activator.CreateInstance(typeof(T), new object[] { ((dynamic)elem).Order, ((dynamic)elem).SeatNumber }))}
                };
        }
    }
}