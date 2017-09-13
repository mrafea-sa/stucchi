using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace _Core.Mvc.Models
{
    public class _BaseModel
    {
        public virtual bool IsEmpty()
        {
            return true;
        }

        public virtual _BaseViewModel GetViewModel()
        {
            return null;
        }

        public virtual dynamic GetDynamicViewModel()
        {
            dynamic viewModel = GetViewModel();
            return viewModel;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns></returns>
        protected virtual SfImage GetImage(Guid imageId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            return librariesManager.GetImages().Where(i => i.Id == imageId).Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault();
        }

        /// <summary>
        /// Gets the selected size URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        protected virtual string GetSelectedSizeUrl(SfImage image)
        {
            if (image.Id == Guid.Empty) return string.Empty;

            string imageUrl;

            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
            var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
            imageUrl = originalImageUrl;

            return imageUrl;
        }


        /// <summary>
        /// Gets the linked page URL.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetLinkedUrl(string pageSelectMode, Guid linkedPageId, string linkedUrl)
        {
            string _linkedUrl = null;

            switch (pageSelectMode)
            {
                case "internal":
                    if (linkedPageId == Guid.Empty) return linkedUrl;
                    var pageManager = PageManager.GetManager();
                    var node = pageManager.GetPageNode(linkedPageId);
                    if (node != null)
                    {
                        string relativeUrl;
                        if (SystemManager.CurrentContext.AppSettings.Multilingual) relativeUrl = node.GetFullUrl(CultureInfo.CurrentUICulture, false);
                        else relativeUrl = node.GetFullUrl();

                        _linkedUrl = UrlPath.ResolveUrl(relativeUrl, true);
                    }
                    break;

                case "external":
                default:
                    if (string.IsNullOrEmpty(linkedUrl)) return linkedUrl;
                    _linkedUrl = linkedUrl;
                    break;
            }

            return _linkedUrl;
        }

        protected virtual IList<dynamic> GetDynamicItems(string jsonItems)
        {
            IList<dynamic> items = new List<dynamic>();

            dynamic dynObjects = JsonConvert.DeserializeObject(jsonItems);

            foreach (var dynObj in dynObjects)
                items.Add(JsonConvert.DeserializeObject(dynObj));

            return items;
        }

        public Video GetVideoNativeAPI(Guid masterVideoId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            Video video = librariesManager.GetVideos().Where(d => d.Id == masterVideoId).FirstOrDefault();

            if (video != null)
            {
                video = librariesManager.Lifecycle.GetLive(video) as Video;
            }
          
            return video;
        }
    }
}
