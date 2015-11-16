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

            //today
            string yyyymmdd = DateTime.Now.ToString("yyyy-MM-dd");
            
            //todays Month
            string mmToday = yyyymmdd.Split('-')[1];

            //Last Month
            string mmExpired = (int.Parse(mmToday) - 1).ToString();

            //Date Last Month
            string expired = yyyymmdd.Replace(mmToday, mmExpired);

            //integer (Date One Month Ago)
            int intExpired = int.Parse(expired.Replace("-", ""));


            List<StebraEntity> archivedNews = new List<StebraEntity>();

            IEnumerable<StebraEntity> archivedNewsEnum = archivedNews;

            //STILL DOES NOT WORK! arrrgh
            //archivedNewsEnum = (from o in news where o.IntDate) < intExpired select o);

            //Example from google
            //var highScores = from student in students
            //                 where student.ExamScores[exam] > score
            //                 select new { Name = student.FirstName, Score = student.ExamScores[exam] };


            return View(archivedNewsEnum);
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