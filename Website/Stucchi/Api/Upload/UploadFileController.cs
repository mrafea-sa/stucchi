using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Workflow;

namespace SitefinityWebApp.Api.Upload
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadFileController : ApiController
    {
        // POST api/<controller>
        public Guid Post()
        {
            Guid documentId = Guid.Empty;

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count < 1)
                return Guid.Empty;
            else
            {
                var postedFile = httpRequest.Files[0];
                string fileExtension = Path.GetExtension(postedFile.FileName);
                documentId = DocumentHelper.CreateDocumentNativeAPI(postedFile.InputStream, fileExtension);
            }

            if (documentId == Guid.Empty)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            return documentId;
        }
    }

    public static class DocumentHelper
    {
        public static Guid CreateDocumentNativeAPI(Stream documentStream, string fileExtension)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            librariesManager.Provider.SuppressSecurityChecks = true;
            WorkflowManager.GetManager().Provider.SuppressSecurityChecks = true;

            Guid documentId = Guid.Empty;
            try
            {
                var documentLibrary = librariesManager.GetDocumentLibraries().Where(x => x.Title == "Image requests uploaded").FirstOrDefault();

                #region create document library
                //create document library if it doesnt exist
                if (documentLibrary == null)
                {
                    documentLibrary = librariesManager.CreateDocumentLibrary();

                    documentLibrary.Title = "Image requests uploaded";
                    documentLibrary.DateCreated = DateTime.UtcNow;
                    documentLibrary.LastModified = DateTime.UtcNow;
                    documentLibrary.UrlName = Regex.Replace(documentLibrary.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                    //Recompiles and validates the url of the library.
                    librariesManager.RecompileAndValidateUrls(documentLibrary);

                    //Save the changes.
                    librariesManager.SaveChanges();
                }
                #endregion

                documentId = Guid.NewGuid();
                var document = librariesManager.CreateDocument(documentId);

                document.Parent = documentLibrary;

                document.Title = "Resume-" + documentId.ToString();
                document.DateCreated = DateTime.UtcNow;
                document.PublicationDate = DateTime.UtcNow;
                document.LastModified = DateTime.UtcNow;
                document.UrlName = Regex.Replace(document.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                document.MediaFileUrlName = Regex.Replace((document.Title + fileExtension).ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                //Upload the document file.
                librariesManager.Upload(document, documentStream, fileExtension);
                //Recompiles and validates the url of the document.
                librariesManager.RecompileAndValidateUrls(document);
                librariesManager.Lifecycle.Publish(document);
                //Save the changes.
                librariesManager.SaveChanges();


                //Publish the DocumentLibraries item. The live version acquires new ID.
                var bag = new Dictionary<string, string>();
                bag.Add("ContentType", typeof(Document).FullName);
                WorkflowManager.MessageWorkflow(documentId, typeof(Document), null, "Publish", false, bag);


            }
            catch (Exception ex)
            {
                Log.Write(ex, System.Diagnostics.TraceEventType.Error);
                return documentId;
            }
            finally
            {
                librariesManager.Provider.SuppressSecurityChecks = false;
                WorkflowManager.GetManager().Provider.SuppressSecurityChecks = false;
            }
            return documentId;
        }
    }
}