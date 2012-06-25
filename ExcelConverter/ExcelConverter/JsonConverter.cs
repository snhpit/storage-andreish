using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Procurios.Public;

namespace ExcelConverter
{
    public class JsonConverter
    {
        private readonly string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

        public void JsonEncode(List<Tuple<string, List<Tuple<string, object>>>> excelObjectData)
        {
            var encode = Json.JsonEncode(excelObjectData);
            var r = excelObjectData.Select(elem => new { ListItemName = elem.Item2.Select(el => el.Item1), ListItem = elem.Item2.Select(el => el.Item2) });
            foreach (var VARIABLE in r)
            {
                for (int i = 0; i < VARIABLE.ListItemName.ToArray().Length; i++)
                {
                    File.AppendAllText(_path + "fileEPPExcel.txt",
                        String.Format(@"{0}", VARIABLE.ListItemName.ToArray()[i] + " - " + VARIABLE.ListItem.ToArray()[i] + "\n"));
                    Console.WriteLine("{0}, {1}", VARIABLE.ListItemName.ToArray()[i].ToString(CultureInfo.InvariantCulture), VARIABLE.ListItem.ToArray()[i]);
                    Console.WriteLine("{0}, {1}", VARIABLE.ListItemName.ToArray()[i], VARIABLE.ListItem.ToArray()[i]);
                }
            }
        }
    }
}
