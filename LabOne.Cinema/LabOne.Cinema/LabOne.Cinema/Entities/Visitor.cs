using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class Visitor : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PasportNumber { get; set; }

        public override string ToString()
        {
            return String.Join("|", ID, FirstName, LastName, PasportNumber);
        }
    }
}