using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExcelConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new ExcelProvider();
            var excelObjectData = provider.GetDataFromEePlus();
            var converter = new JsonConverter();
            converter.JsonEncode(excelObjectData);
        }
    }
}
