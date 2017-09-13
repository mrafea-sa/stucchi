using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity;


namespace _Core
{
    public static class DynamicContentExtensions
    {
        public static IQueryable<DynamicContent> OrderByIds(this IQueryable<DynamicContent> listToOrder, IEnumerable<Guid> orderedIds)
        {
            List<DynamicContent> orderedList = new List<DynamicContent>();
            foreach (Guid id in orderedIds)
            {
                DynamicContent item = listToOrder.Where(pred => pred.GetValue<Guid>("Id") == id).FirstOrDefault();
                if (item != null)
                    orderedList.Add(item);
            }
            return orderedList.AsQueryable();
        }

        public static IList<DynamicContent> OrderByIds(this IList<DynamicContent> listToOrder, IEnumerable<Guid> orderedIds)
        {
            List<DynamicContent> orderedList = new List<DynamicContent>();
            foreach (Guid id in orderedIds)
            {
                DynamicContent item = listToOrder.Where(pred => pred.GetValue<Guid>("Id") == id).FirstOrDefault();
                if (item != null)
                    orderedList.Add(item);
            }
            return orderedList;
        }

        public static IList<Guid> GetListOfIds(this IList<DynamicContent> items)
        {
            IList<Guid> ids = new List<Guid>();

            foreach (DynamicContent item in items)
                ids.Add(item.Id);

            return ids;

        }
        public static Image GetImage(this DynamicContent item, string fieldName)
        {
            Image imageField = null;
            if (item != null)
                imageField = item.GetRelatedItems<Image>(fieldName).FirstOrDefault();

            return imageField;
        }

        public static string GetConcatenatedIds(this IList<DynamicContent> items, string delimitator = " ")
        {
            string ids = string.Empty;

            foreach (DynamicContent item in items)
                ids += (ids == string.Empty ? item.Id.ToString() : delimitator + item.Id.ToString());

            return ids;
        }

        public static bool ContainsElementFromList(this IList<DynamicContent> items, IList<DynamicContent> itemsToBeChecked)
        {
            foreach (var item in items)
                foreach (var itemToBeChecked in itemsToBeChecked)
                    if (item.Id == itemToBeChecked.Id) return true;
            return false;
        }

        public static string GetConcatenatedValues(this IList<DynamicContent> items, string fieldName = "Title", string delimitator = " ", string prefix = "")
        {
            string ids = string.Empty;

            foreach (DynamicContent item in items)
            {
                if (item != null)
                    ids += (ids == string.Empty ? prefix + item.GetValue<Lstring>(fieldName).ToString() : delimitator + prefix + item.GetValue<Lstring>(fieldName).ToString());
            }
            return ids;
        }

        public static void Shuffle(this IList<DynamicContent> itemList)
        {
            int n = itemList.Count;
            Random rng = new Random();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                DynamicContent value = itemList[k];
                itemList[k] = itemList[n];
                itemList[n] = value;
            }
        }

        public static void AddUnique(this List<DynamicContent> self, IEnumerable<DynamicContent> items)
        {
            self.AddRange(items.Where(x => self.FirstOrDefault(y => y.Id == x.Id) == null).ToList());
        }

        public static string GetPageSelectorURL(this DynamicContent item, string fieldName)
        {
            var pageSelectorValue = item.GetValue<Lstring>(fieldName);
            if (!string.IsNullOrEmpty(pageSelectorValue))
            {
                var urlSplit = pageSelectorValue.ToString().Split(';');

                if (urlSplit != null && urlSplit.Any())
                    return urlSplit[0];
            }
            return string.Empty;
        }

        public static Guid GetPageSelectorId(this DynamicContent item, string fieldName)
        {
            var pageSelectorValue = item.GetValue<Lstring>(fieldName);
            if (!string.IsNullOrEmpty(pageSelectorValue))
            {
                var urlSplit = pageSelectorValue.ToString().Split(';');

                if (urlSplit != null && urlSplit.Length > 2)
                    return new Guid(urlSplit[2]);
            }
            return Guid.Empty;
        }
    }
}
