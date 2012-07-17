using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OfficeOpenXml;

namespace ExcelConverter
{
    public class ExcelProvider
    {
        private const string File = "Complex version 20.xlsx";
        private readonly string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory/*, @"..\..\..\"*/));

        public List<Tuple<string, List<Tuple<string, object>>>> GetDataFromEePlus()
        {
            var excelObjectData = new List<Tuple<string, List<Tuple<string, object>>>>();

            using (var stream = System.IO.File.Open(Path.Combine(_path, File), FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = new ExcelPackage(stream))
                {
                    foreach (var workSheet in excelReader.Workbook.Worksheets)
                    {
                        var listOfRows = new List<Tuple<string, object>>();

                        var rowNumbers = workSheet.Dimension.End.Row;
                        var columnNumbers = workSheet.Dimension.End.Column;
                        if (rowNumbers < 2)
                        {
                            for (int i = 1; i <= columnNumbers; i++)
                            {
                                listOfRows.Add(new Tuple<string, object>(workSheet.Cells[1, i].Text, workSheet.Cells[2, i].Text));
                            }
                        }
                        for (int i = 2; i <= rowNumbers; i++)
                        {
                            for (int j = 1; j <= columnNumbers; j++)
                            {
                                DateTime dateTime;
                                var value = workSheet.Cells[i, j].Text;
                                listOfRows.Add(new Tuple<string, object>(workSheet.Cells[1, j].Text,
                                    DateTime.TryParse(value, out dateTime) ? dateTime.ToString(@"M/d/yyyy", CultureInfo.InvariantCulture) : value));
                                if (workSheet.Cells[1, j + 1].Value == null) { break; }
                            }
                            if (workSheet.Cells[i + 1, 1].Value == null) { break; }
                        }

                        excelObjectData.Add(new Tuple<string, List<Tuple<string, object>>>(workSheet.Name, listOfRows));
                    }
                }
            }

            return excelObjectData;
        }
    }
}