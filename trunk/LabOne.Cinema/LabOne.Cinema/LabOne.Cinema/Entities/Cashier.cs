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

        public Cashier() { }

        public Cashier(string id, string firstName, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Cashier(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return String.Join("|", ID, FirstName, LastName);
        }

        public static bool operator ==(Cashier a, Cashier b)
        {
            if (ReferenceEquals(a, b)) return true;
            return a.Equals(b) && b.Equals(a);
        }

        public static bool operator !=(Cashier a, Cashier b)
        {
            return !(a == b);
        }

        public bool Equals(Cashier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FirstName, FirstName) && Equals(other.LastName, LastName)/* && Equals(other.ID, ID)*/;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Cashier)) return false;
            return Equals((Cashier)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FirstName != null ? FirstName.GetHashCode() : 0) * 397) ^
                       (LastName != null ? LastName.GetHashCode() : 0);
            }
        }
    }
}