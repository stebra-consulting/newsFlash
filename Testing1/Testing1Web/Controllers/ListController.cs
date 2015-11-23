using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Testing1Web.Controllers
{
    public class ListController : Controller
    {
     
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult About()
        {
            Uri hostWeb = new Uri("https://stebra.sharepoint.com/sites/SD1");
            string appOnlyAccessToken = TokenHelper.GetS2SAccessTokenWithWindowsIdentity(hostWeb, null);

            //var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(hostWeb.ToString(), appOnlyAccessToken))
            {
                //var contextToken = TokenHelper.GetContextTokenFromRequest(Request);
                //var hostWeb = Request.QueryString["SPHostUrl"];
                //using (var clientContext = TokenHelper.GetClientContextWithContextToken(hostWeb, contextToken, Request.Url.Authority))
                //{
                if (clientContext != null)
                {
                    Web web = clientContext.Web;

                    ListCollection lists = web.Lists;

                    clientContext.Load(lists);
                    clientContext.ExecuteQuery();

                    string myFirstList = "";
                    foreach (List list in lists)
                    {
                        myFirstList += "<p>" + list.Title + "</p>";
                    }

                    

                    ViewBag.mylist = myFirstList;


                }
            }

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}