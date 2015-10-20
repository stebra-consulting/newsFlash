using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using ListToAzureTables01Web.Models;


namespace ListToAzureTables01Web.Services
{
    public class RemoteEventReceiver1 : IRemoteEventService
    {
        /// <summary>
        /// Handles events that occur before an action occurs, such as when a user adds or deletes a list item.
        /// </summary>
        /// <param name="properties">Holds information about the remote event.</param>
        /// <returns>Holds information returned from the remote event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();
            
            switch (properties.EventType)
            {
                case SPRemoteEventType.ItemAdded:
                    HandleItemAdded(properties);
                    break;
                case SPRemoteEventType.ItemUpdated:
                    HandleItemUpdated(properties);
                    break;
                case SPRemoteEventType.ItemDeleted:
                    HandleItemDeleted(properties);
                    break;
                default: break;
            }


            return result;
        }

        private void HandleItemAdded(SPRemoteEventProperties properties)
        {
            using (ClientContext clientContext =
                TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            {

                if (clientContext != null)
                {
                    List news = clientContext.Web.Lists.GetById(
                                properties.ItemEventProperties.ListId);

                    ListItem item = news.GetItemById(
                                properties.ItemEventProperties.ListItemId);

                                  clientContext.Load(item);
                                  clientContext.ExecuteQuery();

                   
                    AddedItem.currentDate = DateTime.Now;


                }


         }
        }
        
        private void HandleItemUpdated(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }

        private void HandleItemDeleted(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Handles events that occur after an action occurs, such as after a user adds an item to a list or deletes an item from a list.
        /// </summary>
        /// <param name="properties">Holds information about the remote event.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            using (ClientContext clientContext = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            {
                if (clientContext != null)
                {   

                    clientContext.Load(clientContext.Web);
                    clientContext.ExecuteQuery();
                }
            }

        }

    }
}
