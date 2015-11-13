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
            //foreach (var item in news)
            //{
            //    string ddmmyyyy_hhmmss = item.Date;

            //    string ddmmyyyy = ddmmyyyy_hhmmss.Split(' ')[0]; 

            //    string dd = ddmmyyyy.Split('/')[0];
            //    string mm = ddmmyyyy.Split('/')[1];
            //    string yyyy = ddmmyyyy.Split('/')[2];

            //    string validyyyymmdd = yyyy +"-"+ mm +"-"+ dd;

            //    item.Date = validyyyymmdd;

            //}

            string yyyymmdd = DateTime.Now.ToString("yyyy-MM-dd");

            //decrease mm by one

            
            //today
            string mmToday = yyyymmdd.Split('-')[1];

            //todays Month
            string mmExpired = (int.Parse(mmToday) - 1).ToString();

            //Date One Month Ago
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