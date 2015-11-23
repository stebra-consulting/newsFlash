using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP = Microsoft.SharePoint.Client;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;

namespace PhotoGatherer1Web.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    List photoList = clientContext.Web.GetList("/sites/SD1/SiteAssets/");


                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View Scope='Recursive'><Query><Where><IsNotNull><FieldRef Name='DocIcon'/></IsNotNull></Where></Query></View>";
                    ListItemCollection photoCollection = photoList.GetItems(camlQuery);

                    clientContext.Load(photoCollection);

                    clientContext.ExecuteQuery();

                    // Retrieve storage account from connection string.
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                        CloudConfigurationManager.GetSetting("StorageConnectionString"));

                    // Create the blob client.
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    // Retrieve reference to a previously created container.
                    CloudBlobContainer container = blobClient.GetContainerReference("photos");



                    foreach (ListItem photo in photoCollection)
                    {
                        bool runPhotoGatherer = false;
                            string FileLeafRef = photo["FileLeafRef"].ToString();
                            string[] spliter = FileLeafRef.Split('.');
                            string fileExtension = spliter[1];
                            string fileName = spliter[0];
                        switch (fileExtension)
                        {
                            case "jpg":
                                runPhotoGatherer = true;
                                break;
                            case "png":
                                runPhotoGatherer = true;
                                break;
                            default:
                                break;
                        }

                        if (runPhotoGatherer)
                        {
                            SP.File file = photo.File;
                            
                            // var fileContents = file.OpenBinaryStream();
                            // Retrieve reference to a blob named "myblob".
                            CloudBlockBlob blockBlob = container.GetBlockBlobReference(FileLeafRef);
                            ClientResult<System.IO.Stream> data = file.OpenBinaryStream();
                            clientContext.Load(file);
                            clientContext.ExecuteQuery();
                            blockBlob.UploadFromStream(data.Value);
                            data.Value.Dispose();                   



                        }

                    }

                }
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
