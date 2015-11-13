using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalWebsite.Models;

namespace ExternalWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            //List to hold news
            List<StebraEntity> freshNews = new List<StebraEntity>();

            //Get news from AzureTable
            freshNews = AzureManager.LoadNews();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}