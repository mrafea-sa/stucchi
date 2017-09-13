using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Extensions
{
    public static class ListingExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static IList<T> ExtractRandomItem<T>(this IList<T> itemList, out T extractedItem)
        {
            extractedItem = itemList[(new Random()).Next(itemList.Count - 1)];
            itemList.Remove(extractedItem);
            return itemList;
        }

        public static bool ContainstList<T>(this IList<T> sourceList, IList<T> checkList)
        {
            if (sourceList != null && sourceList.Any())
            {
                if (checkList != null && checkList.Any())
                {
                    if (sourceList.Count < checkList.Count)
                    {
                        return false;
                    }
                    return !checkList.Except(sourceList).Any();
                }
                return true;
            }

            return false;
        }
    }
}   