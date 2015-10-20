using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using ListToAzureTables01Web.Models;
using Microsoft.Azure;

namespace ListToAzureTables01Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var client = storageAccount.CreateCloudTableClient();

            var stebraTable = client.GetTableReference("StebraList");
            
            var stebraQuery = new TableQuery<StebraEntity>()
               .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "WeAreStebra"));

            var stebras = stebraTable.ExecuteQuery(stebraQuery).ToList();
            

            return View(stebras);
        }

        public ActionResult About()
        {
            var storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var client = storageAccount.CreateCloudTableClient();

            var stebraTable = client.GetTableReference("StebraList");

            if (!stebraTable.Exists())
                {
                    stebraTable.Create();
                }

            TableOperation retrieveOperation = TableOperation.Retrieve<StebraEntity>("WeAreStebra", "ListItem3");

            // Execute the operation.
            TableResult retrievedResult = stebraTable.Execute(retrieveOperation);

            StebraEntity result = (StebraEntity)retrievedResult.Result;

            if (result != null)
            {

                result.ImageUrl = "http://cdn.buzznet.com/assets/users16/crizz/default/funny-pictures-thriller-kitten-impresses--large-msg-121404159787.jpg";
                
                //var awesomeStebra = new StebraEntity("WeAreStebra", "ListItem2");
                //awesomeStebra.ImageUrl = "http://rubmint.com/wp-content/plugins/wp-o-matic/cache/6cb1b_funny-pictures-colur-blind-kitteh-finded-yew-a-pumikin.jpg";

                //var coolStebra = new StebraEntity("WeAreStebra", "ListItem2");
                //coolStebra.ImageUrl = "http://rubmint.com/wp-content/plugins/wp-o-matic/cache/6cb1b_funny-pictures-colur-blind-kitteh-finded-yew-a-pumikin.jpg";

                //var batchOperation = new TableBatchOperation();
                TableOperation batchOperation = TableOperation.InsertOrReplace(result);

                //batchOperation.Insert(awesomeStebra);
                //batchOperation.Insert(coolStebra);
                //stebraTable.ExecuteBatch(batchOperation);
                stebraTable.Execute(batchOperation);
            }
            else
            {
                var awesomeStebra = new StebraEntity("WeAreStebra", "ListItem3");
                awesomeStebra.ImageUrl = "http://rubmint.com/wp-content/plugins/wp-o-matic/cache/6cb1b_funny-pictures-colur-blind-kitteh-finded-yew-a-pumikin.jpg";

                var batchOperation = new TableBatchOperation();
                batchOperation.Insert(awesomeStebra);
                stebraTable.ExecuteBatch(batchOperation);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sharepoint()
        {

            if (AddedItem.currentDate != null)
            {
                ViewBag.listitemAdded = AddedItem.currentDate;
            }
            else
            {
                ViewBag.listitemAdded = "No items have been added";
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}
