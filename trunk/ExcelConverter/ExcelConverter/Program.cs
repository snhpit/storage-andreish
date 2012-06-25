using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExcelConverter
{
    class Program
    {
        private static readonly string Path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

        static void Main(string[] args)
        {
            var provider = new DropBoxProvider();
            //var stream = provider.GetStreamData();
            //provider.GetFromIExcel();

            //provider.GetDataFromExcelInterop();
            
            //var excelObjectData = provider.GetDataFromEePlus();
            
            

            //var r = excelObjectData.Select(elem => new { ListItemName = elem.Item2.Select(el => el.Item1), ListItem = elem.Item2.Select(el => el.Item2) });
            //foreach (var VARIABLE in r)
            //{
            //    for (int i = 0; i < VARIABLE.ListItemName.ToArray().Length; i++)
            //    {
            //        File.AppendAllText(Path + "fileEPPExcel.txt", 
            //            String.Format(@"{0}", VARIABLE.ListItemName.ToArray()[i] + " - " + VARIABLE.ListItem.ToArray()[i] + "\n"));
            //        //Console.WriteLine("{0}, {1}", VARIABLE.ListItemName.ToArray()[i].ToString(CultureInfo.InvariantCulture), VARIABLE.ListItem.ToArray()[i]);
            //        //Console.WriteLine("{0}, {1}", VARIABLE.ListItemName.ToArray()[i], VARIABLE.ListItem.ToArray()[i]);
            //    }
            //}
        }
    }
}
