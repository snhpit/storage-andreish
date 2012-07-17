using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Procurios.Public;

namespace ExcelConverter
{
    public class JsonConverter
    {
        private readonly string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory/*, @"..\..\..\"*/));

        public void JsonEncode(List<Tuple<string, List<Tuple<string, object>>>> excelObjectData)
        {
            foreach (var tuple in excelObjectData)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("var data_{0} = {{ \"{1}\": [", tuple.Item1, "elements");

                if (Equals(tuple.Item2.FirstOrDefault(), null)) { continue; }
                var firstElementName = tuple.Item2.FirstOrDefault().Item1;
                if (Equals(tuple.Item2.LastOrDefault(), null)) { continue; }
                var lastElementName = tuple.Item2.LastOrDefault().Item1;

                foreach (var element in tuple.Item2)
                {
                    if (firstElementName == element.Item1)
                    {
                        stringBuilder.Append("\n\t{");
                    }

                    if (element.Item2 == null || element.Item2.ToString() == String.Empty)
                    {
                        stringBuilder.AppendFormat("\"{0}\":{1}", element.Item1, "null");
                    }
                    else
                    {
                        stringBuilder.AppendFormat(@"""{0}"":""{1}""", element.Item1,
                            element.Item2 is string ? Regex.Replace((string)element.Item2, Environment.NewLine, "\\r\\n") : element.Item2);
                    }

                    if (lastElementName != element.Item1)
                    {
                        stringBuilder.Append(", ");
                    }
                    if (lastElementName == element.Item1)
                    {
                        stringBuilder.Append("}");
                        //tuple.Item2.Count
                        if (element != tuple.Item2.LastOrDefault())
                        {
                            stringBuilder.Append(",");
                        }
                    }
                }
                stringBuilder.Append("\n]};\n");
                File.AppendAllText(_path + "complex version.js", stringBuilder.ToString());
            }
        }
    }
}