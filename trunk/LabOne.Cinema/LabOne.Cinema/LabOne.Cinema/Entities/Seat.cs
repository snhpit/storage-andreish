using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class Seat
    {
        public Order Order { get; set; }

        public int SeatNumber { get; set; }
    }
}