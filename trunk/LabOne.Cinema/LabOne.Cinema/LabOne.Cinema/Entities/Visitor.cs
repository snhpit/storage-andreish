using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class Visitor : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PasportNumber { get; set; }

        //public Visitor(string firstName, string lastName)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //}

        //public Visitor(string id, string firstName, string lastName)
        //{
        //    ID = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //}
    }
}