using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class Film : EntityBase
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string Genre { get; set; }

        //public Film(int id, string title, string year)
        //{
        //    ID = id;
        //    Title = title;
        //    Year = year;
        //}
    }
}