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
    public class HelpDeskOpsMgmt
    {
        public static Guid InsertItem(string firstName, string lastName, string address, string address2, string company,
             string city, string state, string zip, string email, string phone, string message)
        {
            var providerName = String.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            dynamicModuleManager.Provider.SuppressSecurityChecks = true;

            DynamicContent helpDeskSubmissionItem = dynamicModuleManager.CreateDataItem(TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Formsubmissions.Helpdesk"));

            // This is how values for the properties are set
            helpDeskSubmissionItem.SetValue("Title", string.Format("{0} {1}", firstName, lastName));
            helpDeskSubmissionItem.SetValue("FirstName", firstName);
            helpDeskSubmissionItem.SetValue("LastName", lastName);
            helpDeskSubmissionItem.SetValue("Address", address);
            helpDeskSubmissionItem.SetValue("Address2", address2);
            helpDeskSubmissionItem.SetValue("Company", company);
            helpDeskSubmissionItem.SetValue("City", city);
            helpDeskSubmissionItem.SetValue("State", state);
            helpDeskSubmissionItem.SetValue("Zip", zip);
            helpDeskSubmissionItem.SetValue("Phone", phone);
            helpDeskSubmissionItem.SetValue("Email", email);
            helpDeskSubmissionItem.SetValue("Message", message);

            helpDeskSubmissionItem.SetString("UrlName", Guid.NewGuid().ToString());
            helpDeskSubmissionItem.SetValue("PublicationDate", DateTime.Now);

            helpDeskSubmissionItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
            // We can now call the following to publish the item
            ILifecycleDataItem publishedFreeSampleItem = dynamicModuleManager.Lifecycle.Publish(helpDeskSubmissionItem);
            //You need to set appropriate workflow status
            helpDeskSubmissionItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();

            dynamicModuleManager.Provider.SuppressSecurityChecks = false;


            return helpDeskSubmissionItem.Id;
        }
    }
}