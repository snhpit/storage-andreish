using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class Cashier : ItemBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public Cashier(int id, string firstName, string lastName)
        //{
        //    ID = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //}
    }
}