using System;
using System.Collections.Generic;
using System.Configuration;
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
        public string GetData()
        {
            //http://ichart.finance.yahoo.com/table.csv?s=GOOG&a=05&b=1&c=2011&d=05&e=7&f=2011&g=d&ignore=.csv
            HttpWebRequest googleRequest =
                (HttpWebRequest)
                WebRequest.Create("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22MSFT%22%20and%20startDate%20%3D%20%222009-09-11%22%20and%20endDate%20%3D%20%222010-03-10%22&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            HttpWebResponse googleResponse = (HttpWebResponse)googleRequest.GetResponse();
            var googleStream = googleResponse.GetResponseStream();
            if (googleStream == null) return null;
            StreamReader stream = new StreamReader(googleStream);

            return stream.ReadToEnd();
        }
    }
}