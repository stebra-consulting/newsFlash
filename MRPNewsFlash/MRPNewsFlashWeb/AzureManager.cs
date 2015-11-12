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
        //Config
        const string containerName = "photos";
        const string tableName = "stebraNyhetslist";

        //Connection to Azure Storage
        private static CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));


        //BLOB SPECIFIC=========================================================================
        //get Blob -client
        public static CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

        //get Container
        public static CloudBlobContainer container = blobClient.GetContainerReference(containerName);

        public static string ExportImage(System.IO.Stream stream, string blobName)
        {

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.UploadFromStream(stream);

            string url = blockBlob.StorageUri.PrimaryUri.AbsoluteUri;

            return url;

        }

        //TABLE SPECIFIC=========================================================================
        //get Table -client  
        public static CloudTableClient tableClient = StorageAccount.CreateCloudTableClient();

        public static void CreateTable(List<ListItem> stebraList)
        {
            CloudTable table = SelectTable();//make sure table is a clean slate

            var batchOperation = new TableBatchOperation(); //make only one call to Azure Table, use Batch.
            foreach (ListItem item in stebraList)
            {
                //Convert ListItems to Table-entries(Entity)
                var entity = new StebraEntity(
                    "Nyhet",                    //string Stebratype this is partitionKey
                    item["Title"].ToString(),   //string newsEntry this will be used as rowKey
                    "Descriptive text",         //string NewsDescription
                    item["Article"].ToString(), //string NewsArticle
                    item["Datum"].ToString(),   //string NewsDate
                    item["Body"].ToString()     //string NewsBody
                    );

                batchOperation.Insert(entity); //Config Batch
            }
            table.ExecuteBatch(batchOperation); //make only one call to Azure Table, use Batch. this is that one call.
        }

        public static CloudTable SelectTable()
        {
            CloudTable tempTable = null;
            int id = 0;
            while (true)//this does not feel entirely smooth.
            {
                tempTable = tableClient.GetTableReference(tableName + id.ToString()); //check this table
                if (tempTable.Exists())
                {
                    tempTable.Delete(); //delete busy table
                    id++;
                }
                else
                {
                    tempTable.Create();
                    //Table = tempTable; //use this table
                    break;
                }
            }
            return tempTable;
        }
    }

}