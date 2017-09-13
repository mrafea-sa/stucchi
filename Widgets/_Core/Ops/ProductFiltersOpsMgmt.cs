using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace _Core.Ops
{
    public class ProductFiltersOpsMgmt
    {
        public static ICollection<dynamic> GetListOfFilters(string filterCategoryName)
        {
            var items = DynamicContentOps.GetGenericItems(filterCategoryName).ToList();
            ICollection<dynamic> listOfItems = new List<dynamic>();

            foreach (DynamicContent itemContent in items)
            {
                dynamic item = new ExpandoObject();
                item.Id = itemContent.Id.ToString();
                item.Title = HttpUtility.HtmlDecode(itemContent.GetValue<Lstring>("Title").ToString());

                listOfItems.Add(item);
            }

            return listOfItems;
        }
    }
}
