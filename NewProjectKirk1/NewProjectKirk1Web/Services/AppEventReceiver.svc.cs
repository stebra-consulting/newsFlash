using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using NewProjectKirk1Web.Models;
using System.Security;

namespace NewProjectKirk1Web.Services
{
    public class AppEventReceiver : IRemoteEventService
    {

        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            Global.globalError1 += "Hello World";
            SPRemoteEventResult result = new SPRemoteEventResult();
          
            switch (properties.EventType)
            {
                case SPRemoteEventType.AppInstalled:
                    HandleAppInstalled(properties);
                    break;
                case SPRemoteEventType.AppUninstalling:
                    HandleAppUninstalling(properties);
                    break;
                case SPRemoteEventType.ItemAdded:
                    HandleItemAdded(properties);
                    break;
            }


            return result;
        }


        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            Global.globalError1 += "Hello World1";
        }


        /// <summary>
        /// Handles when an app is installed.  Activates a feature in the
        /// host web.  The feature is not required.  
        /// Next, if the Jobs list is
        /// not present, creates it.  Finally it attaches a remote event
        /// receiver to the list.  
        /// </summary>
        /// <param name="properties"></param>
        private void HandleAppInstalled(SPRemoteEventProperties properties)
        {
            //using (ClientContext clientContext =
            //    TokenHelper.CreateAppEventClientContext(properties, false))
            //{
            //    if (clientContext != null)
            //    {
            //        new RemoteEventReceiverManager().AssociateRemoteEventsToHostWeb(clientContext);
            //    }
            //}
        }

        /// <summary>
        /// Removes the remote event receiver from the list and 
        /// adds a new item to the list.
        /// </summary>
        /// <param name="properties"></param>
        private void HandleAppUninstalling(SPRemoteEventProperties properties)
        {
            using (ClientContext clientContext =
                TokenHelper.CreateAppEventClientContext(properties, false))
            {
                if (clientContext != null)
                {
                    new RemoteEventReceiverManager().RemoveEventReceiversFromHostWeb(clientContext);
                }
            }
        }

        /// <summary>
        /// Handles the ItemAdded event by modifying the Description
        /// field of the item.
        /// </summary>
        /// <param name="properties"></param>
        [SharePointContextFilter] //Added by Felix
        private void HandleItemAdded(SPRemoteEventProperties properties)
        {
            Global.globalError1 += "☻ Added New World To Hello ☻ ";
            ClientContext clientContext =
                TokenHelper.CreateRemoteEventReceiverClientContext(properties);//this is null...
            
                if (clientContext != null)
                {
                    new RemoteEventReceiverManager().ItemAddedToListEventHandler(clientContext, properties.ItemEventProperties.ListId, properties.ItemEventProperties.ListItemId);
                }
                
                else if (clientContext == null)
                {
                    clientContext = new ClientContext("https://stebra.sharepoint.com/sites/sd1");

                    if (clientContext != null)
                        {
                    
                        string userName = "simon.bergqvist@stebra.se";

                        SecureString passWord = new SecureString();
                        string passStr = "Simoon123";
                        foreach (char c in passStr.ToCharArray()) passWord.AppendChar(c);

                        clientContext.Credentials = new SharePointOnlineCredentials(userName, passWord);

                        new RemoteEventReceiverManager().ItemAddedToListEventHandler(clientContext, properties.ItemEventProperties.ListId, properties.ItemEventProperties.ListItemId);
                        }
                }

            //☻

            }


    }
}
