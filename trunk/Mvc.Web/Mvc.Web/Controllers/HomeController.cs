using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Practices.ServiceLocation;
using Mvc.Entities;
using Mvc.Web.Converters;
using Mvc.Web.Providers;
using Ninject;
using NinjectAdapter;

namespace Mvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProviderFactory _providerFactory;
        private readonly IConverterFactory _converterFactory;

        public HomeController(IProviderFactory providerFactory, IConverterFactory converterFactory)
        {
            _providerFactory = providerFactory;
            _converterFactory = converterFactory;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public JsonResult Index(InputInfo info)
        {
            if (info.Provider == null || info.Company == null) { return null; }
            var provider = _providerFactory.Create(info.Provider);
            //if (provider == null) { return null; }
            var data = provider.GetData(info.DateFrom, info.DateTo, info.Company);

            if (data == null) { return null; }

            var quotes = _converterFactory.Create(provider.GetType()).Convert(data);
            
            if (quotes == null) { return null;}

            //var quotes = new List<Quote> {
            //    new Quote { Date = DateTime.Parse("11.06.2012"), Close = 29.09, High = 12.12, Low = 121.12, Open = 35.1, Volume = 1412412 },
            //    new Quote { Date = DateTime.Parse("10.06.2012"), Close = 29.09, High = 12.12, Low = 121.12, Open = 35.1, Volume = 1412412 },
            //    new Quote { Date = DateTime.Parse("09.06.2012"), Close = 29.09, High = 12.12, Low = 121.12, Open = 35.1, Volume = 1412412 },
            //};

            return Json(quotes.Select(elem => new
                {
                    Date = elem.Date.ToString("MMM dd, yyyy"),
                    elem.Close,
                    elem.High,
                    elem.Low,
                    elem.Open,
                    elem.Volume
                }));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Weather(string id)
        {
            id = id ?? "Brest,Belarus";
            HttpWebResponse googleResponse = null;
            try
            {
                HttpWebRequest googleRequest = (HttpWebRequest)WebRequest.Create("http://www.google.com/ig/api?weather=" + string.Format(id));
                googleResponse = (HttpWebResponse)googleRequest.GetResponse();
                var g = googleResponse.GetResponseStream();
                XmlDocument googleXmlDoc = new XmlDocument();
                googleXmlDoc.Load(googleResponse.GetResponseStream());

                XmlNode root = googleXmlDoc.DocumentElement;
                XmlNodeList nodeList1 = root.SelectNodes("weather/forecast_information");
                ViewBag.Weather += "<b>City : " + nodeList1.Item(0).SelectSingleNode("city").Attributes["data"].InnerText + "</b>";
                XmlNodeList nodeList = root.SelectNodes("weather/current_conditions");

                ViewBag.Weather += "<table class=\"bordered\" cellpadding=\"5\"><tbody><tr><td><b><big><nobr>" + nodeList.Item(0).SelectSingleNode("temp_c").Attributes["data"].InnerText + " °C | " + nodeList.Item(0).SelectSingleNode("temp_f").Attributes["data"].InnerText + " °F</nobr></big></b>";
                ViewBag.Weather += "<b>Current:</b> " + nodeList.Item(0).SelectSingleNode("condition").Attributes["data"].InnerText + "";
                ViewBag.Weather += " " + nodeList.Item(0).SelectSingleNode("wind_condition").Attributes["data"].InnerText + "";
                ViewBag.Weather += " " + nodeList.Item(0).SelectSingleNode("humidity").Attributes["data"].InnerText;
                nodeList = root.SelectNodes("descendant::weather/forecast_conditions");

                foreach (XmlNode nod in nodeList)
                {
                    ViewBag.Weather += "</td><td align=\"center\">" + nod.SelectSingleNode("day_of_week").Attributes["data"].InnerText + "";
                    ViewBag.Weather += "<img src=\"http://www.google.com" + nod.SelectSingleNode("icon").Attributes["data"].InnerText + "\" alt=\"" + nod.SelectSingleNode("condition").Attributes["data"].InnerText + "\">";
                    ViewBag.Weather += nod.SelectSingleNode("low").Attributes["data"].InnerText + "°F | ";
                    ViewBag.Weather += nod.SelectSingleNode("high").Attributes["data"].InnerText + "°F";
                }
                ViewBag.Weather = ViewBag.Weather + "</td></tr></tbody></table>";
            }
            catch (NullReferenceException ex)
            {
                ViewBag.Weather = ex.Message;
            }
            finally
            {
                googleResponse.Close();
            }
            return View();
        }
    }
}