using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTestFelix.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            int high = 56;
            int low = 4;
            if (high > low) {
                string message = "high is bigger than low";
            }
            string nu = "kom igen funka";
            return "Hello world";
        }
    }
}