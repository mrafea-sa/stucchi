using System;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace _Core.Ops
{
    public class ImageSubmissionOpsMgmt
    {
        public static Guid InsertItem(string fullName, string company, string phoneNumber, string email, string comments)
        {
            var providerName = String.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            dynamicModuleManager.Provider.SuppressSecurityChecks = true;

            DynamicContent imageSubmissionItem = dynamicModuleManager.CreateDataItem(TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Formsubmissions.ImageSubmission"));

            // This is how values for the properties are set
            imageSubmissionItem.SetValue("Title", fullName);
            imageSubmissionItem.SetValue("Name", fullName);
            imageSubmissionItem.SetValue("Company", company);
            imageSubmissionItem.SetValue("PhoneNumber", phoneNumber);
            imageSubmissionItem.SetValue("Email", email);
            imageSubmissionItem.SetValue("Comments", comments);

            imageSubmissionItem.SetString("UrlName", Guid.NewGuid().ToString());
            imageSubmissionItem.SetValue("PublicationDate", DateTime.Now);

            imageSubmissionItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
            // We can now call the following to publish the item
            ILifecycleDataItem publishedFreeSampleItem = dynamicModuleManager.Lifecycle.Publish(imageSubmissionItem);
            //You need to set appropriate workflow status
            imageSubmissionItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();

            dynamicModuleManager.Provider.SuppressSecurityChecks = false;


            return imageSubmissionItem.Id;
        }
    }
}