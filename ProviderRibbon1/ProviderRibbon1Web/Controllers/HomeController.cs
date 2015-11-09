using Microsoft.Azure;
using Microsoft.SharePoint.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using ProviderRibbon1Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProviderRibbon1Web.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            string listGuid = Request.QueryString["SPListId"];
            string title = "";
            string listItemsString = "";
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            if (listGuid != null)
            {

                using (var clientContext = spContext.CreateUserClientContextForSPHost())
                {
                    if (clientContext != null)
                    {
                            listGuid = listGuid.Replace("{", "").Replace("}", "");

                            Guid guid = new Guid(listGuid);

                            List list = clientContext.Web.Lists.GetById(guid);


                            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                        

                            ListItemCollection listItems = list.GetItems(query);
                            clientContext.Load(list);
                            clientContext.Load(listItems);
                            clientContext.ExecuteQuery();


                      

                        var storageAccount = CloudStorageAccount.Parse(
                        CloudConfigurationManager.GetSetting("StorageConnectionString"));

                        var client = storageAccount.CreateCloudTableClient();

                        string nyhetslist1 = "StebraNyhetsList";

                        string nyhetslist2 = "StebraNyhetsList2";

                        var stebraTable = client.GetTableReference(nyhetslist1);
                        var stebraTable2 = client.GetTableReference(nyhetslist2);
                        


                        if (!stebraTable.Exists() && !stebraTable2.Exists())
                        {
                            stebraTable.Create();
                        }

                        else if (!stebraTable.Exists() && stebraTable2.Exists())
                        {
                            stebraTable2.DeleteIfExists();
                            stebraTable.Create();
                        }

                        else if (stebraTable.Exists() && !stebraTable2.Exists())
                        {
                            stebraTable.DeleteIfExists();
                            stebraTable2.Create();                            
                            stebraTable = stebraTable2;
                        }

                       
                        if (stebraTable != null)
                        {

                       
                            foreach (ListItem listItem in listItems)
                            {
                            

                           
                                string itemTitle = listItem["Title"].ToString();
                                string itemBody = listItem["Body"].ToString();
                                string itemArticle = listItem["Article"].ToString();
                                string itemDate = listItem["Datum"].ToString();

                                //Code for encoding 0365-Image


                                //if (itemBody.IndexOf("src=") != -1)
                                //{
                                //    Console.WriteLine("string contains dog!");
                                //}

                                if (itemBody.Contains("src="))
                                {

                                }

                                //    string path = Server.MapPath("~/Content/20151002_113703.jpg");
                                //    byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                                //    string imageBase64Data = Convert.ToBase64String(imageByteData);
                                //    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                                //<div class="ExternalClass365B02E4C74248DF857E2FE109F0C8A0"><p>​Peter skriver till classer och views med viewdata istället för viewbag, det tycker vi på microsoft om.<img class="ms-rte-paste-setimagesize" alt="peter_okt.jpg" src="/sites/SD1/SiteAssets/Lists/Nyhetslista/AllItems/peter_okt.jpg" style="margin&#58;5px;width&#58;130px;height&#58;189px;" />​<br></p></div>



                                    var newsEntry = new StebraEntity("Nyhet", itemTitle, itemBody, itemArticle, itemDate);

                                var batchOperation = TableOperation.InsertOrReplace(newsEntry);
                                stebraTable.Execute(batchOperation);



                            }
                        }




                        title = list.Title;

                }

            }



            ViewBag.titles = listItemsString;
            ViewBag.title = title;
            ViewBag.id = listGuid;

           
            }
            return View();
        }

        public ActionResult About()
        {
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