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
using Microsoft.Office.Interop.Excel;

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

        public void GetDataFromExcelInterop()
        {
            string Path = _path + file;
            
            Application app = new Application();
            //Excel.Worksheet NwSheet;
            Range ShtRange;
            // create the workbook object by opening  the excel file.
            Workbook workBook = app.Workbooks.Open(Path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            // Get The Active Worksheet Using Sheet Name Or Active Sheet
            Worksheet workSheet = (Worksheet)workBook.ActiveSheet;
            int index = 1;
            // that is which cell in the excel you are interesting to read.
            object rowIndex = 1;
            object colIndex1 = 1;
            object colIndex2 = 2;
            object colIndex3 = 3;
            StringBuilder sb = new StringBuilder();
            try
            {
                while (((Range)workSheet.Cells[rowIndex, colIndex1]).Value2 != null)
                {
                    rowIndex = index;
                    string firstName = Convert.ToString(((Range)workSheet.Cells[rowIndex, colIndex1]).Value2);
                    string lastName = Convert.ToString(((Range)workSheet.Cells[rowIndex, colIndex2]).Value2);
                    string Name = Convert.ToString(((Range)workSheet.Cells[rowIndex, colIndex3]).Value2);
                    string line = firstName + "," + lastName + "," + Name;
                    sb.Append(line); sb.Append(Environment.NewLine);
                    Console.WriteLine(" {0},{1},{2} ", firstName, lastName, Name);
                    index++;
                }

                //Writetofile(sb.ToString());

                ShtRange = workSheet.UsedRange;
                Object[,] s = ShtRange.Value;


            }
            catch (Exception ex)
            {
                app.Quit();
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }


        
        public void GetDataFromExcelLibrary()
        {
           

        }

        public List<Tuple<string, List<Tuple<string, object>>>> GetDataFromEePlus()
        {
            var excelObjectData = new List<Tuple<string, List<Tuple<string, object>>>>();

            using (FileStream stream = File.Open(Path.Combine(_path, file), FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage excelReader = new ExcelPackage(stream))
                {
                    foreach (var workSheet in excelReader.Workbook.Worksheets)
                    {
                        var listOfColumns = new List<Tuple<string, object>>();

                        for (int i = 1; i <= workSheet.Dimension.End.Column; i++)
                        {
                            for (int j = 2; j <= workSheet.Dimension.End.Row; j++)
                            {
                                listOfColumns.Add(new Tuple<string, object>(workSheet.Cells[1, i].Text, workSheet.Cells[j, i].Text));
                                if (workSheet.Cells[j + 1, i].Value == null) { break; }
                            }
                            if (workSheet.Cells[1, i + 1].Value == null) { break; }
                        }

                        excelObjectData.Add(new Tuple<string, List<Tuple<string, object>>>(workSheet.Name, listOfColumns));
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
                using(HttpWebResponse googleResponse = (HttpWebResponse)request.GetResponse())
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
