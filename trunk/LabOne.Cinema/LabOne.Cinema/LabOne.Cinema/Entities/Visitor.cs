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

        public Visitor() { }

        public Visitor(string id, string firstName, string lastName, int pasportNumber)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            PasportNumber = pasportNumber;
        }

        public Visitor(string firstName, string lastName, int pasportNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PasportNumber = pasportNumber;
        }

        public override string ToString()
        {
            return String.Join("|", ID, FirstName, LastName, PasportNumber);
        }

        public static bool operator ==(Visitor a, Visitor b)
        {
            if (ReferenceEquals(a, b)) return true;
            return a.Equals(b) && b.Equals(a);
        }

        public static bool operator !=(Visitor a, Visitor b)
        {
            return !(a == b);
        }

        public bool Equals(Visitor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FirstName, FirstName) && Equals(other.LastName, LastName) &&
                   other.PasportNumber == PasportNumber /*&& Equals(other.ID, ID)*/;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Visitor)) return false;
            return Equals((Visitor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (FirstName != null ? FirstName.GetHashCode() : 0);
                result = (result * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                result = (result * 397) ^ PasportNumber;
                return result;
            }
        }
    }
}