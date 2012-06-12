using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Mvc.Entities;

namespace Mvc.Web.Providers
{
    public class GoogleProvider : IProvider
    {
        public string ProviderName { get; set; }

        public string GetData(DateTime dateFrom, DateTime dateTo, string company)
        {
            string data;
            string url =
                string.Format("http://www.google.com/finance/historical?q={2}&startdate={0}&enddate={1}&output=csv",
                dateFrom.ToString("MMM+dd,+yyyy"), dateTo.ToString("MMM+dd,+yyyy"), company ?? "epam");

            HttpWebRequest googleRequest = (HttpWebRequest)WebRequest.Create(url);
            
            HttpWebResponse googleResponse = null;
            try
            {
                googleResponse = (HttpWebResponse)googleRequest.GetResponse();
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Message);
            }
            

            using (var googleStream = googleResponse.GetResponseStream())
            {
                if (googleStream == null)
                {
                    return null;
                }

                using (var stream = new StreamReader(googleStream))
                {
                    data = stream.ReadToEnd();
                }
                googleResponse.Close();
            }
            return data;
        }
    }
}