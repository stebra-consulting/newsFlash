using Microsoft.SharePoint.Client;
using NewProjectKirk1Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;

namespace NewProjectKirk1Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {

            return View();
        }

        [SharePointContextFilter]
        public ActionResult About()
        {
            Global.globalError1 += "Only World No Hello, ";
            using (ClientContext clientContext = new ClientContext("https://stebra.sharepoint.com/sites/sd1"))
            {


                if (clientContext != null)
                {

                    string userName = "simon.bergqvist@stebra.se";

                    SecureString passWord = new SecureString();
                    string passStr = "Simoon123";
                    foreach (char c in passStr.ToCharArray()) passWord.AppendChar(c);

                    clientContext.Credentials = new SharePointOnlineCredentials(userName, passWord);
                    new RemoteEventReceiverManager().AssociateRemoteEventsToHostWeb(clientContext);
                }
            }

            //        var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            //using (var clientContext = spContext.CreateUserClientContextForSPHost())
            //{ new RemoteEventReceiverManager().AssociateRemoteEventsToHostWeb(clientContext); }
            
            ViewBag.RemoteEvent = " Hello globalError: " + Global.globalError + " \n globalError1: " + Global.globalError1;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
