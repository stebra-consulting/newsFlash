using Microsoft.Azure;
using Microsoft.SharePoint.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MRPNewsFlashWeb.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MRPNewsFlashWeb
{
    public class AzureManager
    {
        //Configure target container / table
        const string containerName = "photos";
        const string tableName = "stebraNyhetslist";

        //Connection to Azure Storage
        private static CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

        //get Table -client  
        public static CloudTableClient tableClient = StorageAccount.CreateCloudTableClient();

        //get Blob -client
        public static CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

        //get Container
        public static CloudBlobContainer container = blobClient.GetContainerReference(containerName);

        public static string ExportImage(System.IO.Stream stream, string blobName) { //returns url to blob


            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.UploadFromStream(stream);

            string url = blockBlob.StorageUri.PrimaryUri.AbsoluteUri;

            return url;

        }

        //CloudBlobContainer container = blobClient.GetContainerReference("photos");

        
        
        //property that holds the apps Tablereference
        //public static CloudTable NewsTable { get; set; }

        ////Create a new Azure Table for this App. workaround from having to wait to delete table with same name
        //public static void SelectTable()
        //{
        //    int id = 0;
        //    while (true)
        //    {
        //        var tempTable = tableClient.GetTableReference("NewsTable" + id.ToString()); //check this table
        //        if (tempTable.Exists())
        //        {
        //            //tempTable.Delete(); //Delete tables manually for now. via serverexplorer in VS
        //            id++;
        //        }
        //        else
        //        {
        //            tempTable.Create();
        //            NewsTable = tempTable; //use this table
        //            break;
        //        }
        //    }
        //}

        ////save news to azure table from inparameter listItems
        //public static void SaveNews(ListItemCollection listItems)
        //{

        //    var batchOperation = new TableBatchOperation(); //make only one call to Azure Table, use Batch.
        //    foreach (ListItem item in listItems)
        //    {
        //        //Convert ListItems to Table-entries(Entity)
        //        var entity = new StebraEntity(
        //            "News",                     //string Stebratype
        //            item["Title"].ToString(),   //string newsEntry
        //            "Descriptive text",         //string NewsDescription
        //            item["Article"].ToString(), //string NewsArticle
        //            item["Datum"].ToString(),   //string NewsDate
        //            item["Body"].ToString()     //string NewsBody
        //            );

        //        batchOperation.Insert(entity); //Batch this
        //    }
        //    NewsTable.ExecuteBatch(batchOperation); //Execute Batch
        //}

        ////Load all news from Azure Table, return stebras
        //public static IEnumerable<StebraEntity> LoadAllNews()
        //{

        //    //Query all entities where "PartitionKey" is "News"
        //    var allNewsQuery = new TableQuery<StebraEntity>()
        //        .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "News"));

        //    //Entities to List
        //    var stebras = NewsTable.ExecuteQuery(allNewsQuery).ToList();

        //    //Return List
        //    return stebras;

        //}


    }
}