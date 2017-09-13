using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.Application.Pages
{
    [ServiceContract]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class PagesServiceCustom
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        public ItemPageSpeedInsights[] GetItemsPageSpeedInsights(Guid[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                var manager = PageManager.GetManager();

                var data = manager.GetPageNodes().Where(n => ids.Contains(n.Id))
                    .Select(n => new ItemPageSpeedInsights()
                                {
                                    Id = n.Id,
                                    PageSpeedInsights = n.GetValue<string>("PageSpeedInsights")
                                }).ToArray();

                return data;
            }

            return null;
        }
    }

    [DataContract]
    public class ItemPageSpeedInsights
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string PageSpeedInsights { get; set; }
    }
}