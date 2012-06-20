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

namespace ExcelConverter
{
    public class DropBoxProvider
    {
        private string url = "https://dl.dropbox.com/u/34287000/Complex%20version%2018.xlsx";
        private string file = "Complex version 18.xlsx";
        private string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

        public void GetFromIExcel()
        {
            using (FileStream stream = File.Open(Path.Combine(path, file), FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    // попробовать сохранить поток, а потом открыть CreateBinaryReader, возможно проблемы с AsDataSet

                    excelReader.IsFirstRowAsColumnNames = true;
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

        public void GetFromEePlus()
        {
            using (FileStream stream = File.Open(Path.Combine(path, file), FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage excelReader = new ExcelPackage(stream))
                {
                    var value = excelReader.Workbook.Worksheets.First().Cells.Value;
                    var firstWorkSheet = excelReader.Workbook.Worksheets.First().Cells.Select(elem => elem.First());
                    foreach (var item in firstWorkSheet)
                    {
                        Console.WriteLine(item.Text);
                    }
                      
                    Console.WriteLine();
                }
            }
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
