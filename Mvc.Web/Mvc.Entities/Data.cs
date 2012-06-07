using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Entities
{
    public class Data
    {
        DateTime Date { get; set; }
        double Open { get; set; }
        double High { get; set; }
        double Low { get; set; }
        double Close { get; set; }
        double Volume { get; set; }
    }
}