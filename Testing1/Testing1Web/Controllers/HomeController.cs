using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Testing1Web.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            User spUser = null;

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    spUser = clientContext.Web.CurrentUser;
                    var web = clientContext.Web;


                    clientContext.Load(spUser, user => user.Title, user => user.LoginName);
                    clientContext.Load(web, Web => Web.Title);
                    clientContext.ExecuteQuery();

                    ViewBag.UserName = spUser.Title;
                    ViewBag.LoginName = spUser.LoginName;
                    ViewBag.WebTitle = web.Title;
                }
            }

            return View();
        }

        public ActionResult About()
        {
            var context = HttpContext;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
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
