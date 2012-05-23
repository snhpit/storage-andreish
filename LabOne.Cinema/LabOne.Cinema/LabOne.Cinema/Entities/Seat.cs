using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class Seat
    {
        public const int MaxSeats = 80;

        public Order Order { get; set; }

        public int SeatNumber { get; set; }

        public Seat() { }

        public Seat(Order order, int seatNumber)
        {
            Order = order;
            SeatNumber = seatNumber;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}", Order.ID, SeatNumber);
        }

        public static bool operator ==(Seat a, Seat b)
        {
            if (ReferenceEquals(a, b)) return true;
            return a.Equals(b) && b.Equals(a);
        }

        public static bool operator !=(Seat a, Seat b)
        {
            return !(a == b);
        }

        public bool Equals(Seat other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Order, Order) && other.SeatNumber == SeatNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Seat)) return false;
            return Equals((Seat)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Order != null ? Order.GetHashCode() : 0) * 397) ^ SeatNumber;
            }
        }
    }
}