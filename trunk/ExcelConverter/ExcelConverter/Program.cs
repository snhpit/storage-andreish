using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new DropBoxProvider();
            //var stream = provider.GetStreamData();
            //provider.GetFromIExcel();
            provider.GetFromEePlus();
        }
    }
}
