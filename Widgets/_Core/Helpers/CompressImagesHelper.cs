using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using System.IO;
using Telerik.Sitefinity.Workflow;
using _Core.ImageCompression;


namespace _Core.Helpers
{
    public class CompressImagesHelper
    {
        static LibrariesManager librariesManager = LibrariesManager.GetManager();


        public static void CompressAllImages(string apiKey, ImageCompressionEngineType compressionEngine, string secretKey = null)
        {
            var allMasterImages = GetAllMasterImages();
            IImageCompressionApi compressionApiEngine = ImageCompressionEngine.GetCompressionApi(compressionEngine, apiKey, secretKey);

            foreach (var img in allMasterImages)
            {
                if (img.GetValue<string>("CompressionType") == ImageCompressionEngine.GetApiTag(compressionEngine)) continue;

                using (var stream = librariesManager.Download(img.Id))
                {
                    byte[] bytesInStream = new byte[stream.Length];
                    stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                    var responseImageBytes = compressionApiEngine.CompressImage(bytesInStream);

                    if (responseImageBytes != null)
                    {
                        UpdateImage(img, responseImageBytes, compressionEngine);
                    }
                    else
                    {
                        Log.Write(string.Format("Compress for {0} failed. URL of the image: {1}", img.Id.ToString(), img.MediaUrl));
                    }
                }
            }
        }

        public static List<Image> GetAllMasterImages()
        {
            return librariesManager.GetImages().Where(x => x.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master).ToList();
        }

        public static bool UpdateImage(Image master, byte[] responseImage, ImageCompressionEngineType compressionEngine)
        {
            librariesManager.Provider.SuppressSecurityChecks = true;
            WorkflowManager.GetManager().Provider.SuppressSecurityChecks = true;
            try
            {
                var imageExtension = master.Extension;

                var workFlowStatus = master.ApprovalWorkflowState.ToString();

                Image temp = librariesManager.Lifecycle.CheckOut(master) as Image;
                temp.SetValue("CompressionType", ImageCompressionEngine.GetApiTag(compressionEngine));

                Stream stream = new MemoryStream(responseImage);

                librariesManager.Upload(temp, stream, imageExtension);

                //checkin the temp
                master = librariesManager.Lifecycle.CheckIn(temp) as Image;
                librariesManager.SaveChanges();

                //publish if master was published
                if (workFlowStatus == "Published")
                {
                    var bag = new Dictionary<string, string>();
                    bag.Add("ContentType", typeof(Image).FullName);
                    WorkflowManager.MessageWorkflow(master.Id, typeof(Image), null, "Publish", false, bag);
                }
                Log.Write(string.Format("SUCCESS: Update for image {0} success. URL of the image: {1}", master.Id, master.MediaUrl));
            }
            catch (Exception ex)
            {
                throw ex;
                Log.Write(string.Format("Update for image {0} failed. URL of the image: {1}", master.Id, master.MediaUrl));
                Log.Write(ex.Message);
                return false;
            }
            finally
            {
                librariesManager.Provider.SuppressSecurityChecks = false;
                WorkflowManager.GetManager().Provider.SuppressSecurityChecks = false;
            }
            return true;
        }
    }
}