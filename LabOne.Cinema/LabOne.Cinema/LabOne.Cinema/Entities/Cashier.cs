using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class Cashier : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return String.Join("|", ID, FirstName, LastName);
        }
    }
}