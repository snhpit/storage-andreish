using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Mvc.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

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
                XmlDocument googleXmlDoc = new XmlDocument();
                googleXmlDoc.Load(googleResponse.GetResponseStream());
                XmlNode root = googleXmlDoc.DocumentElement;
                XmlNodeList nodeList1 = root.SelectNodes("weather/forecast_information");
                ViewBag.HavaDurumu = ViewBag.HavaDurumu + "<b>City : " + nodeList1.Item(0).SelectSingleNode("city").Attributes["data"].InnerText + "</b>";
                XmlNodeList nodeList = root.SelectNodes("weather/current_conditions");

                ViewBag.HavaDurumu = ViewBag.HavaDurumu + "<table class=\"bordered\" cellpadding=\"5\"><tbody><tr><td><b><big><nobr>" + nodeList.Item(0).SelectSingleNode("temp_c").Attributes["data"].InnerText + " °C | " + nodeList.Item(0).SelectSingleNode("temp_f").Attributes["data"].InnerText + " °F</nobr></big></b>";
                ViewBag.HavaDurumu = ViewBag.HavaDurumu + "<b>Current:</b> " + nodeList.Item(0).SelectSingleNode("condition").Attributes["data"].InnerText + "";
                ViewBag.HavaDurumu = ViewBag.HavaDurumu + " " + nodeList.Item(0).SelectSingleNode("wind_condition").Attributes["data"].InnerText + "";
                ViewBag.HavaDurumu = ViewBag.HavaDurumu + " " + nodeList.Item(0).SelectSingleNode("humidity").Attributes["data"].InnerText;
                nodeList = root.SelectNodes("descendant::weather/forecast_conditions");
                foreach (XmlNode nod in nodeList)
                {
                    ViewBag.HavaDurumu = ViewBag.HavaDurumu + "</td><td align=\"center\">" + nod.SelectSingleNode("day_of_week").Attributes["data"].InnerText + "";
                    ViewBag.HavaDurumu = ViewBag.HavaDurumu + "<img src=\"http://www.google.com" + nod.SelectSingleNode("icon").Attributes["data"].InnerText + "\" alt=\"" + nod.SelectSingleNode("condition").Attributes["data"].InnerText + "\">";
                    ViewBag.HavaDurumu = ViewBag.HavaDurumu + nod.SelectSingleNode("low").Attributes["data"].InnerText + "°F | ";
                    ViewBag.HavaDurumu = ViewBag.HavaDurumu + nod.SelectSingleNode("high").Attributes["data"].InnerText + "°F";
                }
                ViewBag.HavaDurumu = ViewBag.HavaDurumu + "</td></tr></tbody></table>";
            }
            catch (System.Exception ex)
            {
                ViewBag.HavaDurumu = ex.Message;
            }
            finally
            {
                googleResponse.Close();
            }
            return View();
        }
    }
}
