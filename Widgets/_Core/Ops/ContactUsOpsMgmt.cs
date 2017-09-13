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
    public class ContactUsOpsMgmt
    {
        public static Guid InsertItem(string firstName, string lastName, string address, string address2, string company,
             string city, string state, string zip, string country, string phone, string message, string reasonsForContact, string hasOptedForSubscription)
        {
            var providerName = String.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            dynamicModuleManager.Provider.SuppressSecurityChecks = true;

            DynamicContent contactSubmissionItem = dynamicModuleManager.CreateDataItem(TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Formsubmissions.Contact"));

            // This is how values for the properties are set
            contactSubmissionItem.SetValue("Title", string.Format("{0} {1}", firstName, lastName));
            contactSubmissionItem.SetValue("FirstName", firstName);
            contactSubmissionItem.SetValue("LastName", lastName);
            contactSubmissionItem.SetValue("Address", address);
            contactSubmissionItem.SetValue("Address2", address2);
            contactSubmissionItem.SetValue("Company", company);
            contactSubmissionItem.SetValue("City", city);
            contactSubmissionItem.SetValue("State", state);
            contactSubmissionItem.SetValue("Zip", zip);
            contactSubmissionItem.SetValue("Phone", phone);
            contactSubmissionItem.SetValue("Country", country);
            contactSubmissionItem.SetValue("Message", message);
            contactSubmissionItem.SetValue("ReasonsForContact", reasonsForContact);
            contactSubmissionItem.SetValue("HasOptedForSubscription", hasOptedForSubscription);

            contactSubmissionItem.SetString("UrlName", Guid.NewGuid().ToString());
            contactSubmissionItem.SetValue("PublicationDate", DateTime.Now);

            contactSubmissionItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
            // We can now call the following to publish the item
            ILifecycleDataItem publishedFreeSampleItem = dynamicModuleManager.Lifecycle.Publish(contactSubmissionItem);
            //You need to set appropriate workflow status
            contactSubmissionItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();

            dynamicModuleManager.Provider.SuppressSecurityChecks = false;


            return contactSubmissionItem.Id;
        }
    }
}