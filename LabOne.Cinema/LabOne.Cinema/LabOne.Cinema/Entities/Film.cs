using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class Film
    {
        public int ID { get; private set; }

        public string Title { get; private set; }

        public string Year { get; private set; }

        public Film(int id, string title, string year)
        {
            ID = id;
            Title = title;
            Year = year;
        }
    }
}