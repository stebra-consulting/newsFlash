using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
        
            
            return View();
        }

        public ActionResult About()
        {
            
            return View();
          
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            string itemBody = "< div class=\"ExternalClass365B02E4C74248DF857E2FE109F0C8A0\"><p>​Peter skriver till classer och views med viewdata istället för viewbag, det tycker vi på microsoft om.<img class=\"ms-rte-paste-setimagesize\" alt=\"peter_okt.jpg\" src=\"/wp-content/uploads/2013/05/cardinal-best-ever.jpg\" style=\"margin&#58;5px;width&#58;130px;height&#58;189px;\" />​<br></p></div>";


            if (itemBody.Contains("src"))
            {
                string[] splitstring = itemBody.Split(' ');
                foreach (string part in splitstring)
                {
                    if (part.Contains("src"))
                    {
                        string[] srcParts = part.Split('"');

                        string sub = srcParts[1];

                        string fullpath = "http://www.invisiblechildren.org" + sub;
                        //var request = WebRequest.Create("http://www.invisiblechildren.org/wp-content/uploads/2013/05/cardinal-best-ever.jpg");
                        var request = WebRequest.Create(fullpath);
                        var response = request.GetResponse();
                        var stream = response.GetResponseStream();

                        Image img = Bitmap.FromStream(stream);

                        byte[] imageByteData = ImageToByte(img);
                        string imageBase64Data = Convert.ToBase64String(imageByteData);
                        string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);


                        itemBody = itemBody.Replace(sub, imageBase64Data);
                        ViewBag.body = itemBody;
                        ViewBag.sub = sub;
                        ViewBag.base64 = imageBase64Data;


                        ViewBag.image = imageDataURL;
                    }
                }
            }
            return View();
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
       
    }
}