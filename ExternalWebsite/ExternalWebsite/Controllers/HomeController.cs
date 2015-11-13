using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExternalWebsite.Models;
using System.Globalization;

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

            //sort list descend by Dateprop
            freshNews = freshNews.OrderByDescending(item => Convert.ToDateTime(item.Date)).ToList();

            return View(freshNews);
        }
        public ActionResult Archived()
        {
            //list to hold Archived News
            List<StebraEntity> news = new List<StebraEntity>();

            //Get news from AzureTable
            news = AzureManager.LoadNews();

            //sort list descend by Dateprop
            news = news.OrderByDescending(item => Convert.ToDateTime(item.Date)).ToList();

            //Make string item.Date sortable as an (int) and 
            //TASK: update item date string to make it valid as an inParameter in Convert.ToDateTime() AND sortable as an int
            //converts "08/11/2015 23:00:00" to "2015-11-08"
            foreach (var item in news)
            {
                string ddmmyyyy_hhmmss = item.Date;

                string ddmmyyyy = ddmmyyyy_hhmmss.Split(' ')[0]; 

                string dd = ddmmyyyy.Split('/')[0];
                string mm = ddmmyyyy.Split('/')[1];
                string yyyy = ddmmyyyy.Split('/')[2];

                string validyyyymmdd = yyyy +"-"+ mm +"-"+ dd;

                item.Date = validyyyymmdd;

            }

            string today = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime expiredDate = new DateTime();
            expiredDate = Convert.ToDateTime(today);
            expiredDate.AddMonths(-1);

            //var today = date.
            //var latest = today.AddHours(-validHours);
            //var archivedNews = (from o in news where (int)o.Date < expiredDate select o);


            return View(news);
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