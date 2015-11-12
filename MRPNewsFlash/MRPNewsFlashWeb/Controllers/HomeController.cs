using Microsoft.SharePoint.Client;
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
            //Give SPManager HttpContext, (only avaible in this Controller)
            SPManager.CurrentHttpContext = HttpContext;

            //GetItemCollection
            ListItemCollection items = SPManager.GetItemCollection("Nyhetslista");

            //get Image from filename
            string FileLeafRef = "peter_okt.jpg";
            using (var fileStream = SPManager.GetImage(FileLeafRef))
            {
                //createBlob 
                AzureManager.CreateBlob(fileStream, "ImageFromStream");
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
