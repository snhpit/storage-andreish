using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Excel;
using OfficeOpenXml;
using Koogra = Net.SourceForge.Koogra;

namespace ExcelConverter
{
    public class DropBoxProvider
    {
        private string url = "https://dl.dropbox.com/u/34287000/Complex%20version%2018.xlsx";
        private string file = "Complex version 18.xlsx";
        private readonly string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

        public void GetFromIExcel()
        {
            using (FileStream stream = File.Open(Path.Combine(_path, file), FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    // попробовать сохранить поток, а потом открыть CreateBinaryReader, возможно проблемы с AsDataSet

                    //excelReader.IsFirstRowAsColumnNames = true;
                    DataSet result = excelReader.AsDataSet();

                    var value = result.Tables[0].Rows[0];

                    var categoryName = result.Tables["Events"];

                    var xml = result.GetXml();
                    XDocument document = XDocument.Parse(xml);
                    //document.Save(path + "file.txt");


                    //Console.WriteLine(document);
                }
            }
        }

        public void GetDataFromKoogra()
        {
            using (FileStream stream = File.Open(Path.Combine(_path, file), FileMode.Open, FileAccess.Read))
            {
                var workBook = new Koogra.Excel2007.Workbook(stream);
                var sheets = workBook.GetWorksheets();
                foreach (var worksheet in sheets)
                {
                    for (var r = worksheet.CellMap.FirstRow; r <= worksheet.CellMap.LastRow; ++r)
                    {
                        var row = worksheet.GetRow(r);

                        for (var c = worksheet.CellMap.FirstCol; c <= worksheet.CellMap.LastCol; ++c)
                        {
                            Console.WriteLine(row.GetCell(c).Value);
                            Console.WriteLine(row.GetCell(c).GetFormattedValue());
                        }
                    }
                }
            }
        }

        public void GetDataFromExcelMapper()
        {

        }

        public List<Tuple<string, List<Tuple<string, string>>>> GetDataFromEePlus()
        {
            var excelObjectData = new List<Tuple<string, List<Tuple<string, string>>>>();

            using (FileStream stream = File.Open(Path.Combine(_path, file), FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage excelReader = new ExcelPackage(stream))
                {
                    foreach (var workSheet in excelReader.Workbook.Worksheets)
                    {
                        var listOfColumns = new List<Tuple<string, string>>();

                        for (int i = 1; i <= workSheet.Dimension.End.Column; i++)
                        {
                            for (int j = 2; j <= workSheet.Dimension.End.Row; j++)
                            {
                                listOfColumns.Add(new Tuple<string, string>(workSheet.Cells[1, i].Text, workSheet.Cells[j, i].Text));
                                if (workSheet.Cells[j + 1, i].Value == null) { break; }
                            }
                            if (workSheet.Cells[1, i + 1].Value == null) { break; }
                        }
                        excelObjectData.Add(new Tuple<string, List<Tuple<string, string>>>(workSheet.Name, listOfColumns));
                    }
                }
            }
            return excelObjectData;
        }

        public string GetStreamData()
        {
            string data = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                using (HttpWebResponse googleResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var q = googleResponse.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(q))
                        {
                            using (BinaryReader reader = new BinaryReader(q))
                            {
                                //var chars = reader.ReadChars(100);
                            }

                        }
                    }
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Message);
            }

            return data;
        }

        public string GetStringData()
        {
            string data = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse googleResponse = (HttpWebResponse)request.GetResponse();
                using (var googleStream = googleResponse.GetResponseStream())
                {
                    using (var stream = new StreamReader(googleStream))
                    {
                        data = stream.ReadToEnd();
                    }
                    googleResponse.Close();
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Message);
            }

            return data;
        }
    }
}
