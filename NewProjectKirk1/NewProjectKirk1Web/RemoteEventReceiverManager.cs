using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;
using System.Configuration;
using NewProjectKirk1Web.Models;

namespace NewProjectKirk1Web
{
    public class RemoteEventReceiverManager
    {
        private const string RECEIVER_NAME = "ItemAddedEvent";
        private const string LIST_TITLE = "TestList4";
        

        public void AssociateRemoteEventsToHostWeb(ClientContext clientContext)
        {
            if (Global.globalY != 1)
            {

                Global.globalError += "28-";
            //Add Push Notification Feature to HostWeb
            //Not required here, just a demonstration that you
            //can activate features.
            clientContext.Web.Features.Add(
                     new Guid("41e1d4bf-b1a2-47f7-ab80-d5d6cbba3092"),
                     true, FeatureDefinitionScope.None);

                Global.globalError += "36-";
                //Get the Title and EventReceivers lists
                clientContext.Load(clientContext.Web.Lists,
                lists => lists.Include(
                    list => list.Title,
                    list => list.EventReceivers).Where
                        (list => list.Title == LIST_TITLE));

            clientContext.ExecuteQuery();
                Global.globalError += "45-";
                List jobsList = clientContext.Web.Lists.FirstOrDefault();

#if (DEBUG)
            // In debug mode we will delete the existing list, so we prevent our system from orphaned event receicers.
            // RemoveEventReceiversFromHostWeb is sometimes not called in debug mode and/or the app id has changed. 
            // On RER registration SharePoint adds the app id to the event registration information, and you are only able 
            // to remove the event with the same app where it was registered. Also note that you would need to completely 
            // uninstall an app before SharePoint will trigger the appuninstalled event. From the documentation: 
            // The **AppUninstalling** event only fires when a user completely removes the add-in: the add-in needs to be deleted 
            // from the site recycle bins in an end-user scenario. In a development scenario the add-in needs to be removed from 
            // the “Apps in testing” library.

            if (null != jobsList)
            {
                jobsList.DeleteObject();
                clientContext.ExecuteQuery();
                jobsList = null;
            }
#endif
                Global.globalError += "65-";
                bool rerExists = false;
            if (null == jobsList)
            {

                //List does not exist, create it
                jobsList = CreateJobsList(clientContext);
                    Global.globalError += "72-";
                }
                
            else
            {
                foreach (var rer in jobsList.EventReceivers)
                {
                    if (rer.ReceiverName == RECEIVER_NAME)
                    {
                        rerExists = true;
                        System.Diagnostics.Trace.WriteLine("Found existing ItemAdded receiver at "
                            + rer.ReceiverUrl);
                    }
                }
            }
                Global.globalError += "87-";
                if (!rerExists)
            {

                EventReceiverDefinitionCreationInformation receiver =
                    new EventReceiverDefinitionCreationInformation();
                receiver.EventType = EventReceiverType.ItemAdded;

                    //Get WCF URL where this message was handled
                    //System.ServiceModel.OperationContext op = System.ServiceModel.OperationContext.Current;
                    //Message msg = op.RequestContext.RequestMessage;
                    //receiver.ReceiverUrl = msg.Headers.To.ToString();

                    //Simons Instance
                    //receiver.ReceiverUrl = "https://serviceberra.servicebus.windows.net/314700446/821505743/obj/d91500b0-1033-4876-bf0c-fa8a607e8eca/Services/AppEventReceiver.svc";

                    receiver.ReceiverUrl = "https://serviceberra.servicebus.windows.net/73036694/1885843836/obj/d91500b0-1033-4876-bf0c-fa8a607e8eca/Services/AppEventReceiver.svc";

                    Global.globalError += "101";
                    receiver.ReceiverName = RECEIVER_NAME;
                receiver.Synchronization = EventReceiverSynchronization.Synchronous;

                //Add the new event receiver to a list in the host web
                jobsList.EventReceivers.Add(receiver);
                clientContext.ExecuteQuery();

                    Global.globalError += "109-";
                    Global.globalError += (" Added ItemAdded receiver at " + receiver.ReceiverUrl + " - ");

            }
                Global.globalY++;
                
                FirstTimeInstall(clientContext);
            }
        }

        public void FirstTimeInstall(ClientContext clientContext)
        {
            List nyhetsLista = clientContext.Web.Lists.GetByTitle(LIST_TITLE);

            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View><Query><Where><IsNotNull><FieldRef Name='Title'/>" +
                "</IsNotNull></Where></Query></View>";
            ListItemCollection items = nyhetsLista.GetItems(camlQuery);

           
            clientContext.Load(items);
            clientContext.ExecuteQuery();

           
            //foreach (ListItem item in items)
            //{
            if (Global.globalX != items.Count)
            {
                var storageAccount = CloudStorageAccount.Parse(
                   CloudConfigurationManager.GetSetting("StorageConnectionString"));

                var client = storageAccount.CreateCloudTableClient();

                var stebraTable = client.GetTableReference("StebraNyhetsList");

                if (!stebraTable.Exists())
                {
                    stebraTable.Create();
                }
                int i = 0;
                
                for (i = 0; i < items.Count; i++)
                {

                    ListItem item = items[i];
                    string itemTitle = item["Title"].ToString();
                    string itemBody = item["Body"].ToString();
                    string itemArticle = item["Article"].ToString();
                    string itemDate = item["Date"].ToString();


                    var newsEntry = new StebraEntity("Nyhet", itemTitle, itemBody, itemArticle, itemDate);

                    var batchOperation = TableOperation.InsertOrReplace(newsEntry);
                    stebraTable.Execute(batchOperation);

                    Global.globalX++;
                }

                
                
            }
           
            
        }

        public void RemoveEventReceiversFromHostWeb(ClientContext clientContext)
        {
            List myList = clientContext.Web.Lists.GetByTitle(LIST_TITLE);
            clientContext.Load(myList, p => p.EventReceivers);
            clientContext.ExecuteQuery();

            var rer = myList.EventReceivers.Where(
                e => e.ReceiverName == RECEIVER_NAME).FirstOrDefault();

            try
            {
                System.Diagnostics.Trace.WriteLine("Removing ItemAdded receiver at "
                        + rer.ReceiverUrl);

                //This will fail when deploying via F5, but works
                //when deployed to production
                rer.DeleteObject();
                clientContext.ExecuteQuery();

            }
            catch (Exception oops)
            {
                System.Diagnostics.Trace.WriteLine(oops.Message);
            }

            //Now the RER is removed, add a new item to the list
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem newItem = myList.AddItem(itemCreateInfo);
            newItem["Title"] = "App deleted";
            newItem["Body"] = "Deleted on " + System.DateTime.Now.ToLongTimeString();
            newItem.Update();

            clientContext.ExecuteQuery();

        }

        public void ItemAddedToListEventHandler(ClientContext clientContext, Guid listId, int listItemId)
        {
            try
            {
                List news = clientContext.Web.Lists.GetById(listId);
                ListItem item = news.GetItemById(listItemId);
                clientContext.Load(item);
                clientContext.ExecuteQuery();

                string itemTitle = item["Title"].ToString();
                string itemBody = item["Body"].ToString();
                string itemArticle = item["Article"].ToString();
                string itemDate = item["Date"].ToString();

                var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

                var client = storageAccount.CreateCloudTableClient();

                var stebraTable = client.GetTableReference("StebraNyhetsList");

                if (!stebraTable.Exists())
                {
                    stebraTable.Create();
                }

                var newsEntry = new StebraEntity("Nyhet", itemTitle, itemBody, itemArticle, itemDate);
                
                var batchOperation = new TableBatchOperation();
                batchOperation.Insert(newsEntry);
                stebraTable.ExecuteBatch(batchOperation);
                Global.globalError += "242-";
            }

            catch (Exception oops)
            {
                System.Diagnostics.Trace.WriteLine(oops.Message);
                Global.globalError += "248-";
            }
            Global.globalError += "250-";
        }

        /// <summary>
        /// Creates a list with Description and AssignedTo fields
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 
        internal List CreateJobsList(ClientContext context)
        {

            ListCreationInformation creationInfo = new ListCreationInformation();
            creationInfo.Title = LIST_TITLE;

            creationInfo.TemplateType = (int)ListTemplateType.GenericList;
            List list = context.Web.Lists.Add(creationInfo);
            list.Description = "List of jobs and assignments";
            list.Fields.AddFieldAsXml("<Field DisplayName='Body' Type='Text' />",
                true,
                AddFieldOptions.DefaultValue);
            list.Fields.AddFieldAsXml("<Field DisplayName='Article' Type='Text' />",
               true,
               AddFieldOptions.DefaultValue);
            list.Fields.AddFieldAsXml("<Field DisplayName='Date' Type='Text' />",
                true,
                AddFieldOptions.DefaultValue);

            list.Update();

            //Do not execute the call.  We simply create the list in the context, 
            //it's up to the caller to call ExecuteQuery.
            return list;
        }
    }
}