using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace AzureStorageLibrary
{
    public class AzureStorage
    {
        // Retrieve the storage account from the connection string.
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

        // Create the table client.
        CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        // Create the table if it doesn't exist.
        CloudTable table = tableClient.GetTableReference("people");
        table.CreateIfNotExists();

    }
}
