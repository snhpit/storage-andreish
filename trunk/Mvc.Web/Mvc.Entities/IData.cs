using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Entities
{
    public interface IData
    {
        DateTime Date { get; set; }

        double Volume { get; set; }
    }
}