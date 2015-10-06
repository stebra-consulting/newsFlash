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
            string nu = "kom igen funka";
            return "Hello world";
        }
    }
}