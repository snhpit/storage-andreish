using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class Seat
    {
        public Order Order { get; set; }

        public int SeatNumber { get; set; }

        public override string ToString()
        {
            return string.Format("{0}|{1}", Order.ID, SeatNumber);
        }
    }
}