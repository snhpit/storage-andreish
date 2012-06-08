using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Mvc.Entities;

namespace Mvc.Web.Providers
{
    public class GoogleProvider : IProvider
    {
        public string GetData()
        {
            HttpWebRequest googleRequest =
                (HttpWebRequest)
                WebRequest.Create("http://www.google.com/finance/historical?cid=694653&startdate=Jun+1%2C+2011&enddate=Jun+7%2C+2011&output=csv");
            HttpWebResponse googleResponse = (HttpWebResponse)googleRequest.GetResponse();
            var googleStream = googleResponse.GetResponseStream();
            if (googleStream == null) return null;
            StreamReader stream = new StreamReader(googleStream);
            return stream.ReadToEnd();
            //finally
            //{
            //    googleResponse.Close();
            //    googleStream.Close();
            //    stream.Close();
            //}
        }
    }
}