using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class Order : EntityBase
    {
        public Visitor Visitor { get; set; }

        public Cashier Cashier { get; set; }

        public Film Film { get; set; }

        public DateTime DateOrder { get; set; }
    }
}