using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class Film : EntityBase
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        //public Film(int id, string title, string year)
        //{
        //    ID = id;
        //    Title = title;
        //    Year = year;
        //}

        public override string ToString()
        {
            return String.Join("|", ID, Title, Year, Genre);
        }
    }
}