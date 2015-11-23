using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExternalWebsite2.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace ExternalWebsite2
{
    public class AzureManager
    {
        //Config
        private const string tableName = "stebraNyhetslist";
        private const string partitionKey = "Nyhet";

        //Connection to Azure Storage
        private static CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

        //get Table -client  
        public static CloudTableClient tableClient = StorageAccount.CreateCloudTableClient();

        public static List<StebraEntity> LoadNews()
        {

            CloudTable table = SelectValidTable();

            //Query all entities where "PartitionKey" is "News"
            var allNewsQuery = new TableQuery<StebraEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));//partitionKey is "Nyhet"

            //Entities to List
            var news = table.ExecuteQuery(allNewsQuery).ToList();

            if (LinkManager.globalNews == null)
            {
                LinkManager.globalNews = news;

            }

            //Return List
            return news;


        }


        public static CloudTable SelectValidTable()
        {
            CloudTable tempTable = null;

            for (int id = 0; id < 2; id++)
            {
                tempTable = tableClient.GetTableReference(tableName + id.ToString());
                if (tempTable.Exists()) break;
            }
            //check this tables

            return tempTable;
        }
    }

}

//public static void CreateTable(List<ListItem> stebraList)
//{
//    CloudTable table = SelectTable();//make sure table is a clean slate

//    var batchOperation = new TableBatchOperation(); //make only one call to Azure Table, use Batch.
//    foreach (ListItem item in stebraList)
//    {
//        //Convert ListItems to Table-entries(Entity)
//        var entity = new StebraEntity(
//            "Nyhet",                    //string Stebratype this is partitionKey
//            item["Title"].ToString(),   //string newsEntry this will be used as rowKey
//            "Descriptive text",         //string NewsDescription
//            item["Article"].ToString(), //string NewsArticle
//            item["Datum"].ToString(),   //string NewsDate
//            item["Body"].ToString()     //string NewsBody
//            );

//        batchOperation.Insert(entity); //Config Batch
//    }
//    table.ExecuteBatch(batchOperation); //make only one call to Azure Table, use Batch. this is that one call.
//}