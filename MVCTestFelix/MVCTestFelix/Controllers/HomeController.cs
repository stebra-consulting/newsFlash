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
            int t1 = 54;
            int t2 = 123;
            int t3 = 0;

            string nu = "kom igen funka";
            if (t1 < t2)
            {
                nu = "kom igen funka inte";
            }
           
            return "Hello world";
        }
    }
}