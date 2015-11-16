﻿using System;
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

            //sortlist by latestFirst
            freshNews = SortByDateManager.LatestFirst(freshNews);

            return View(freshNews);
        }
        public ActionResult Archived()
        {
            //list to hold Archived News
            List<StebraEntity> news = new List<StebraEntity>();

            //Get news from AzureTable
            news = AzureManager.LoadNews();

            //sort list descend by Dateprop
            news = SortByDateManager.LatestFirst(news);

            //return news that is older than 1 month
            news = SortByDateManager.ByMonth(news);

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