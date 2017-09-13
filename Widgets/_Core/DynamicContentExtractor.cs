using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

using _Core.Extensions;
using Telerik.Sitefinity.Model;
using _Core.Helpers;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;

using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Modules.Pages;
using System.Globalization;
using Telerik.Sitefinity.Web;
using _Core.Mvc.Models;


namespace _Core
{
    public class DynamicContentExtractor
    {

        #region Private Static Methods

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns></returns>
        private static SfImage GetImage(Guid imageId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            var img = librariesManager.GetImages().Where(i => i.Id == imageId);
            var test2 =img.Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>());
            return librariesManager.GetImages().Where(i => i.Id == imageId).Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault();
        }

        /// <summary>
        /// Gets the selected size URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        private static string GetSelectedSizeUrl(SfImage image)
        {
            if (image.Id == Guid.Empty) return string.Empty;

            string imageUrl;

            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
            var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
            imageUrl = originalImageUrl;

            return imageUrl;
        }

        #endregion


        public static List<ExpandoObject> GetMediaAssets(DynamicContent itemContent, string[] fields)
        {
            List<ExpandoObject> mediaInfo = new List<ExpandoObject>();

            foreach (string field in fields)
            {
                var fieldImage = itemContent.GetImage(field);
                if (fieldImage != null)
                {
                    dynamic mediaInfoItem = new ExpandoObject();
                    mediaInfoItem.Src = fieldImage.MediaUrl.ConvertToImageSeo();
                    mediaInfoItem.Alt = fieldImage.AlternativeText;
                    mediaInfoItem.Title = fieldImage.Title;
                    mediaInfoItem.Width = fieldImage.Width;
                    mediaInfoItem.Height = fieldImage.Height;

                    mediaInfo.Add(mediaInfoItem);
                }
            }

            return mediaInfo;
        }

        public static List<ExpandoObject> GetMediaAssets(string dynamicContentType, Guid objId, string[] fields)
        {
            DynamicContent itemContent = DynamicContentOps.GetGenericItemById(dynamicContentType, objId);
            return GetMediaAssets(itemContent, fields);
        }

        public static List<ExpandoObject> GetLinks(DynamicContent itemContent, string[] fields)
        {
            List<ExpandoObject> linksInfo = new List<ExpandoObject>();

            foreach (string field in fields)
            {
                string fieldVal = itemContent.GetValue<Lstring>(field).ToString();

                if (fieldVal.IndexOf(";") >= 0)
                {
                    dynamic linksInfoItem = new ExpandoObject();
                    if (fieldVal.StartsWith("mailto")) linksInfoItem.Url = fieldVal.Substring(0, fieldVal.IndexOf(";"));
                    else linksInfoItem.Url = Utility.ConvertRelativeUrlToAbsoluteUrl(fieldVal.Substring(0, fieldVal.IndexOf(";")));

                    linksInfo.Add(linksInfoItem);
                }
            }

            return linksInfo;

        }

        public static List<ExpandoObject> GetLinks(string dynamicContentType, Guid objId, string[] fields)
        {
            DynamicContent itemContent = DynamicContentOps.GetGenericItemById(dynamicContentType, objId);
            return GetLinks(itemContent, fields);

        }

        public static _MediaModel GetMediaModel(Guid imageId)
        {
            _MediaModel mediaModel = new _MediaModel();

            SfImage image;
            if (imageId != Guid.Empty)
            {
                image = GetImage(imageId);
                if (image != null)
                {
                    mediaModel.ImageSelectedSizeUrl = GetSelectedSizeUrl(image).ConvertToImageSeo();
                    mediaModel.ImageAlternativeText = image.AlternativeText;
                    mediaModel.ImageTitle = image.Title;
                }
            }

            return mediaModel;
        }


        public static List<ExpandoObject> GetPages(DynamicContent itemContent, string[] fields)
        {
            List<ExpandoObject> pagesInfo = new List<ExpandoObject>();

            foreach (string field in fields)
            {
                var pages = itemContent.GetRelatedItems(field).ToList();

                foreach (var page in pages)
                {
                    PageNode pageNode = page as PageNode;
                    if (pageNode == null) continue;

                    dynamic pageInfoItem = new ExpandoObject();
                    pageInfoItem.Url = pageNode.GetFullUrl(CultureInfo.CurrentUICulture, true);
                    pageInfoItem.Title = pageNode.Title;
                    pageInfoItem.PageNode = pageNode;
                   
                    pagesInfo.Add(pageInfoItem);
                }

            }
            return pagesInfo;
        }


    }
}
