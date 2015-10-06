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
            int t1 = 54;
            int t2 = 123;
            if (t1 < t2)
            {

            }
            string nu = "kom igen funka";
            return "Hello world";
        }
    }
}