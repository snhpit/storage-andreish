using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using Mvc.Entities;

namespace Mvc.Web.Providers
{
    public class YahooProvider : IProvider
    {
        public string GetData(DateTime dateFrom, DateTime dateTo, string company)
        {
            //http://finance.yahoo.com/q/hp?s=EPAM&a=01&b=8&c=2012&d=05&e=8&f=2012&g=d&ignore=.csv

            string data = null;

            if (dateTo.Year == 1) { dateTo = DateTime.Now; }

            string url = string.Format(
                "http://query.yahooapis.com/v1/public/yql?q=" +
                "select%20*%20from%20yahoo.finance.historicaldata%20" +
                "where%20symbol%20%3D%20%22{2}%22%20and%20" +
                "startDate%20%3D%20%22{0}%22%20and%20" +
                "endDate%20%3D%20%22{1}%22&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys", 
                dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"), company /*?? "epam"*/);

            HttpWebRequest googleRequest = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                HttpWebResponse googleResponse = (HttpWebResponse)googleRequest.GetResponse();
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
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
            }

            return data;
        }
    }
}