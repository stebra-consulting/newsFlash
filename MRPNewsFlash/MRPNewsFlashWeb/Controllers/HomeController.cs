﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRPNewsFlashWeb.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MRPNewsFlashWeb.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            
            //SPManager.CurrentHttpContext = HttpContext;
            //ListItemCollection items = SPManager.GetItemCollection("Nyhetslista");

            //string FileLeafRef = "peter_okt.jpg";
            //using (var fileStream = SPManager.GetImage(FileLeafRef))
            //{
                
            //    AzureManager.CreateBlob(fileStream, "ImageFromStream");
            //}

            return View();
        }

        public ActionResult Publish()
        {//catch Publish URL-Parameters

            string listGuid = Request.QueryString["SPListId"];

            SPManager.CurrentHttpContext = HttpContext;
            ListItemCollection items = SPManager.GetItemsFromGuid(listGuid);

            foreach (ListItem item in items)
            {
                ListItem scannedAndReplaced = StringScanner.ScanningListItem(item);
            }

            return View();
        }
        public ActionResult About()
        {
            //not yet implemented
            //IEnumerable<StebraEntity> news = AzureTableManager.LoadAllNews();

            return View();//news
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
