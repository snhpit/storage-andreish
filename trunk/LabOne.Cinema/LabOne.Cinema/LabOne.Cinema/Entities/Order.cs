using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class Order : EntityBase
    {
        public Order()
        {
            DateOrder = new DateTime();
        }

        public Visitor Visitor { get; set; }

        public Cashier Cashier { get; set; }

        public Film Film { get; set; }

        public DateTime DateOrder { get; set; }

        public override string ToString()
        {
            return String.Join("|", ID, Visitor.FirstName, Visitor.LastName, Film.Title, Film.Year, Cashier.LastName, DateOrder);
        }

        //public void Add(Cashier listCashier)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerator GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}