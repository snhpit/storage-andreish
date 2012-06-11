using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Mvc.Entities;
using Mvc.Web.Converters;
using Mvc.Web.Providers;
using Ninject;

namespace Mvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConverter _converter;
        private IProvider _provider;

        public HomeController(IProvider provider, IConverter converter)
        {
            _converter = converter;
            _provider = provider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(InputInfo info)
        {
            ViewBag.Message = "Finance Statistic";

            var data = _provider.GetData(info.DateFrom, info.DateTo, info.Company);
            var quotes = new Quote { Date = "11.06.2012", Close = 29.09, High = 12.12, Low = 121.12, Open = 35.1, Volume = 1412412 };//_converter.Convert(data);
            ViewData["Quotes"] = new List<Quote> { quotes };

            if (Request.IsAjaxRequest())
            {
                return Json(new List<Quote> { quotes });
            }

            return View();
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