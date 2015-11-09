using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string path = Server.MapPath("~/Content/20151002_113703.jpg");
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            ViewBag.image = imageDataURL;
            ViewBag.base64 = imageBase64Data;
            return View();
        }

        public ActionResult About()
        {
            
            return View();
          
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
    }
}