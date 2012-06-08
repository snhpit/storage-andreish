using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Entities
{
    public class InputInfo
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string Provider { get; set; }

        public string Company { get; set; }
    }
}