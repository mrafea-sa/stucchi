using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace _Core
{
    public static class DynamicContentOps
    {
        /// <summary>
        /// Get related items from a DynamicContent item in Master status.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="sourceItem"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IQueryable<DynamicContent> GetGenericMasterRelatedItems(string providerName, DynamicContent sourceItem, string fieldName)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(sourceItem);

            return sourceItem.GetRelatedItems<DynamicContent>(fieldName);
        }

        /// <summary>
        /// Get related items from a DynamicContent item in Live status.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="sourceItem"></param>
        /// <param name="fieldName"></param>
        /// <param name="sourceLifecycleStatus">Use Deleted to avoid translating an item to another lifecyle version. The given version will be used.</param>
        /// <returns></returns>
        public static IList<DynamicContent> GetGenericLiveRelatedItems(string providerName, DynamicContent sourceItem, string fieldName, ContentLifecycleStatus sourceLifecycleStatus = ContentLifecycleStatus.Master)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            switch (sourceLifecycleStatus)
            {
                case ContentLifecycleStatus.Live:
                    sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetLive(sourceItem);
                    break;
                case ContentLifecycleStatus.Master:
                    sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(sourceItem);
                    break;
                case ContentLifecycleStatus.Temp:
                    sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetTemp(sourceItem);
                    break;
                default:
                    break;
            }
            var masterItems = sourceItem.GetRelatedItems<DynamicContent>(fieldName);

            IList<DynamicContent> liveList = new List<DynamicContent>();

            foreach (var masterItem in masterItems)
            {
                liveList.Add((DynamicContent)dynamicModuleManager.Lifecycle.GetLive(masterItem));
            }

            return liveList;
        }

        /// <summary>
        /// Get related items of a DynamicContent. You can chose to query master/live versions.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="sourceItem"></param>
        /// <param name="fieldName"></param>
        /// <param name="useSourceConversion"></param>
        /// <param name="sourceLifecycleStatus"></param>
        /// <param name="useTargetConversion"></param>
        /// <param name="targetLifecycleStatus"></param>
        /// <returns></returns>
        public static IQueryable<DynamicContent> GetGenericRelatedItems(
            DynamicContent sourceItem,
            string fieldName,
            bool useSourceConversion = false,
            int sourceLifecycleStatus = (int)ContentLifecycleStatus.Master,
            bool useTargetConversion = false,
            int targetLifecycleStatus = (int)ContentLifecycleStatus.Master)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(sourceItem.Provider.ToString());
            if (useSourceConversion)
            {
                switch (sourceLifecycleStatus)
                {
                    case (int)ContentLifecycleStatus.Live:
                        sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetLive(sourceItem);
                        break;
                    case (int)ContentLifecycleStatus.Master:
                        sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(sourceItem);
                        break;
                    case (int)ContentLifecycleStatus.Temp:
                        sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetTemp(sourceItem);
                        break;
                    default:
                        break;
                }
            }

            var relatedItems = sourceItem.GetRelatedItems<DynamicContent>(fieldName);
            if (!useTargetConversion)
            {
                return relatedItems;
            }
            else if (relatedItems.Any())
            {
                dynamicModuleManager = DynamicModuleManager.GetManager(relatedItems.First().Provider.ToString());
                switch (targetLifecycleStatus)
                {
                    case (int)ContentLifecycleStatus.Live:
                        return relatedItems.ToList().Select(p => (DynamicContent)dynamicModuleManager.Lifecycle.GetLive(p, null)).AsQueryable();
                    case (int)ContentLifecycleStatus.Master:
                        return relatedItems.ToList().Select(p => (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(p)).AsQueryable();
                    case (int)ContentLifecycleStatus.Temp:
                        return relatedItems.ToList().Select(p => (DynamicContent)dynamicModuleManager.Lifecycle.GetTemp(p, null)).AsQueryable();
                    default:
                        return relatedItems;
                }

            }
            else
            {
                return relatedItems;
            }
        }

        public static IQueryable<DynamicContent> GetGenericParentRelatedItems(
            string parentProviderName,
            DynamicContent sourceItem,
            string fieldName,
            string parentItemType,
            bool isVisible = true,
            bool useSourceConversion = false,
            int sourceLifecycleStatus = (int)ContentLifecycleStatus.Master,
            bool useTargetConversion = false,
            int targetLifecycleStatus = (int)ContentLifecycleStatus.Master)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(sourceItem.Provider.ToString());
            if (useSourceConversion)
            {
                switch (sourceLifecycleStatus)
                {
                    case (int)ContentLifecycleStatus.Live:
                        sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetLive(sourceItem);
                        break;
                    case (int)ContentLifecycleStatus.Master:
                        sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(sourceItem);
                        break;
                    case (int)ContentLifecycleStatus.Temp:
                        sourceItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetTemp(sourceItem);
                        break;
                    default:
                        break;
                }
            }

            var relatedItems = sourceItem
                .GetRelatedParentItems(parentItemType, parentProviderName, fieldName)
                .OfType<DynamicContent>();

            if (isVisible)
                relatedItems = relatedItems
                    .Where("Visible = true");

            if (!useTargetConversion)
            {
                return relatedItems;
            }
            else
            {
                switch (targetLifecycleStatus)
                {
                    case (int)ContentLifecycleStatus.Live:
                        return relatedItems.ToList().Select(p => (DynamicContent)dynamicModuleManager.Lifecycle.GetLive(p, null)).AsQueryable();
                    case (int)ContentLifecycleStatus.Master:
                        return relatedItems.ToList().Select(p => (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(p)).AsQueryable();
                    case (int)ContentLifecycleStatus.Temp:
                        return relatedItems.ToList().Select(p => (DynamicContent)dynamicModuleManager.Lifecycle.GetTemp(p, null)).AsQueryable();
                    default:
                        return relatedItems;
                }
            }
        }

        /// <summary>
        /// Get related items from a DynamicContent item id in Master status.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="typeName"></param>
        /// <param name="sourceItemId">The item id.</param>
        /// <param name="fieldName">The field name associated to the relation.</param>
        /// <returns></returns>
        public static IQueryable<DynamicContent> GetGenericMasterRelatedItems(string providerName, string typeName, Guid sourceItemId, string fieldName)
        {
            DynamicContent currentItem = GetGenericItemById(typeName, sourceItemId, providerName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            currentItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(currentItem);

            return currentItem.GetRelatedItems<DynamicContent>(fieldName);
        }

        /// <summary>
        /// Get related items from a DynamicContent item id in Live status.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="typeName"></param>
        /// <param name="sourceItemId">The item id.</param>
        /// <param name="fieldName">The field name associated to the relation.</param>
        /// <returns></returns>
        public static IList<DynamicContent> GetGenericLiveRelatedItems(string providerName, string typeName, Guid sourceItemId, string fieldName)
        {
            DynamicContent currentItem = GetGenericItemById(typeName, sourceItemId, providerName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            currentItem = (DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(currentItem);
            var masterItems = currentItem.GetRelatedItems<DynamicContent>(fieldName);

            IList<DynamicContent> liveList = new List<DynamicContent>();

            foreach (var masterItem in masterItems)
            {
                liveList.Add((DynamicContent)dynamicModuleManager.Lifecycle.GetLive(masterItem));
            }

            return liveList;
        }

        /// <summary>
        /// Get an IQueriable of DynamicContent items which are visible.
        /// </summary>
        /// <param name="providerName">The name of the provider you want data retrieved from.</param>
        /// <param name="itemTypeName">The type name of the items you want to retrieve.</param>
        /// <param name="status">ContentLifecycleStatus of the items.</param>
        /// <returns></returns>
        public static IQueryable<DynamicContent> GetGenericItems(string itemTypeName, bool fromCache = false, ContentLifecycleStatus status = ContentLifecycleStatus.Live, bool isVisible = true, string providerName = "")
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemType = TypeResolutionService.ResolveType(itemTypeName);

            var result = dynamicModuleManager.GetDataItems(itemType)
                .Where(p => p.Status == status)
                .Where(p => p.Visible == isVisible);

            return result;
        }

        /// <summary>
        /// Get an IQueriable of DynamicContent items which are visible.
        /// </summary>
        /// <param name="providerName">The name of the provider you want data retrieved from.</param>
        /// <param name="itemTypeName">The type name of the items you want to retrieve.</param>
        /// <param name="status">ContentLifecycleStatus of the items.</param>
        /// <returns></returns>
        public static IList<DynamicContent> GetGenericItemsList(string itemTypeName, bool fromCache = false, ContentLifecycleStatus status = ContentLifecycleStatus.Live, bool isVisible = true, string providerName = "")
        {
            if (fromCache && DynamicContentCache.GetInstance().ExistsContent(itemTypeName))
                return DynamicContentCache.GetInstance().GetContent(itemTypeName) as IList<DynamicContent>;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemType = TypeResolutionService.ResolveType(itemTypeName);

            var result = dynamicModuleManager.GetDataItems(itemType)
                .Where(p => p.Status == status)
                .Where(p => p.Visible == isVisible).ToList();

            if (fromCache) DynamicContentCache.GetInstance().AddContent(itemTypeName, result);

            return result;
        }

        /// <summary>
        /// Query items by title.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="itemTypeName"></param>
        /// <param name="status"></param>
        /// <param name="title"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static IQueryable<DynamicContent> GetGenericItemsByTitle(string providerName, string itemTypeName, ContentLifecycleStatus status, string title, bool isVisible = true)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemType = TypeResolutionService.ResolveType(itemTypeName);

            var result = dynamicModuleManager.GetDataItems(itemType)
                .Where(p => p.Status == status)
                .Where(p => p.Visible == isVisible)
                .Where(string.Format("Title = \"{0}\"", title));

            return result;
        }

        /// <summary>
        /// Retrieves a DynamicContent item by its id.
        /// </summary>
        /// <param name="providerName">The name of the provider you want data retrieved from.</param>
        /// <param name="itemTypeName">The type name of the items you want to retrieve.</param>
        /// <param name="itemId">Guid of the item.</param>
        /// <returns></returns>
        public static DynamicContent GetGenericItemById(string itemTypeName, Guid itemId, string providerName = "")
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemType = TypeResolutionService.ResolveType(itemTypeName);

            DynamicContent currentItem = dynamicModuleManager.GetDataItem(itemType, itemId);

            return currentItem;
        }

        /// <summary>
        /// Deletes a dynamic item or a translation of a dynamic item.
        /// </summary>
        /// <param name="providerName">The provider to delete dynamic content item inside.</param>
        /// <param name="itemTypeName">The dynamic type name of the item.</param>
        /// <param name="sourceItem">The item to delete.</param>
        /// <param name="cultureName">The culture version to delete.</param>
        public static void DeleteGenericItem(string providerName, string itemTypeName, DynamicContent sourceItem, string cultureName = null)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemTtype = TypeResolutionService.ResolveType(itemTypeName);

            dynamicModuleManager.DeleteDataItem((DynamicContent)dynamicModuleManager.Lifecycle.GetMaster(sourceItem), new CultureInfo(cultureName));

            dynamicModuleManager.SaveChanges();
        }

        /// <summary>
        /// Delete all dynamic items.
        /// </summary>
        /// <param name="providerName">The provider to delete dynamic content item inside.</param>
        /// <param name="itemTypeName">The dynamic type name of the item.</param>
        public static void DeleteAllGenericItems(string providerName, string itemTypeName)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemTtype = TypeResolutionService.ResolveType(itemTypeName);

            dynamicModuleManager.DeleteDataItems(itemTtype);

            dynamicModuleManager.SaveChanges();
        }


        /// <summary>
        /// Retrieve a provider by siteId and moduleName.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="moduleName"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static string GetDynamicProvider(Guid siteId, string moduleName, bool isDefault = true)
        {
            if (!SystemManager.CurrentContext.IsMultisiteMode)
                return null;

            var currentContext = (MultisiteContext)SystemManager.CurrentContext;

            var selectedSite = currentContext.GetSites().FirstOrDefault(p => p.Id == siteId);
            if (selectedSite == null)
                throw new ArgumentException("The specified site could not be found.");

            var selectedProvider = selectedSite.GetProviders(moduleName).FirstOrDefault(p => p.IsDefault == isDefault);

            return selectedProvider == null ? null : selectedProvider.ProviderName;
        }

        /// <summary>
        /// Get all the providers from a site.
        /// </summary>
        /// <param name="siteName">The site name you want to search for providers in.</param>
        /// <returns></returns>
        public static IList<MultisiteContext.SiteDataSourceLinkProxy> GetDynamicProviderListBySiteName(string siteName)
        {
            if (!SystemManager.CurrentContext.IsMultisiteMode)
                return null;

            var currentContext = (MultisiteContext)SystemManager.CurrentContext;
            var currentSite = SystemManager.CurrentContext.CurrentSite;

            currentContext.ChangeCurrentSite(currentContext.GetSiteByName(siteName));
            var result = SystemManager.CurrentContext.CurrentSite.SiteDataSourceLinks;
            currentContext.ChangeCurrentSite(currentSite);

            return result;
        }
    }
}
