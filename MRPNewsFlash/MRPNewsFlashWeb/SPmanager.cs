using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MRPNewsFlashWeb
{
    public static class SPManager
    {
        public static HttpContextBase CurrentHttpContext { get; set; }
        public static ListItemCollection GetItemCollection(string listName)
        {
            ListItemCollection items = null;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(CurrentHttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    //get listdata from sharepoint
                    List list = clientContext.Web.Lists.GetByTitle(listName);
                    clientContext.Load(list);
                    clientContext.ExecuteQuery();

                    //get all listitems that has a title-column
                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = @"
                                        <View>
                                            <Query>
                                                <Where>
                                                    <IsNotNull>
                                                        <FieldRef Name='Title' />
                                                    </IsNotNull>
                                                </Where>
                                            </Query>
                                        </View>";
                    items = list.GetItems(camlQuery);

                    clientContext.Load(items);
                    clientContext.ExecuteQuery();
                    
                }
            }
            return items;
        }
        public static System.IO.Stream GetImage(string fileLeafRef)
        {
            System.IO.Stream stream = null;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(CurrentHttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    List photoList = clientContext.Web.GetList("/sites/SD1/SiteAssets/");

                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = @"<View Scope='Recursive'>
                                            <Query>
                                                <Where>
                                                    <Eq>
                                                      <FieldRef Name='FileLeafRef'></FieldRef>
                                                      <Value Type='Text'>" + fileLeafRef + @"</Value>
                                                     </Eq>
                                                 </Where>
                                             </Query>
                                         </View>";
                    ListItemCollection photoCollection = photoList.GetItems(camlQuery);

                    clientContext.Load(photoCollection);

                    clientContext.ExecuteQuery();

                    ListItem item = photoCollection.ElementAt(0);

                    File file = item.File;

                    ClientResult<System.IO.Stream> data = file.OpenBinaryStream();

                    clientContext.Load(file);
                    clientContext.ExecuteQuery();//timeconsuming process?

                    stream = data.Value;//timeconsuming process?

                }
                return stream; //call stream too fast in Controller and it throws error
            }

        }

    }
}