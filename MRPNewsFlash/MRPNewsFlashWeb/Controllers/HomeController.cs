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

            //coming from Publish Ribbon button

            //Fetch all images from site assets as itemcollection use caml
            //put in global variable

            //fetch news as itemcollection from "nyhetslista"-list

            //check if each news contains image
            //check if that image is public/0365
            //upload 0365images to azblob
            //replace img src attr in stebraentities.



            //init azure blob
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("photos");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("FromManager.jpg");
            //init azure blob


            ListItemCollection items = SPManager.GetItemCollection("Nyhetslista", HttpContext);
            string FileLeafRef = "peter_okt.jpg";
            System.IO.Stream fileStream = SPManager.GetImage(FileLeafRef, HttpContext);




            blockBlob.UploadFromStream(fileStream);

            fileStream.Dispose();


            return View();
        }

        public ActionResult About()
        {
            IEnumerable<StebraEntity> news = AzureTableManager.LoadAllNews();

            return View(news);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
