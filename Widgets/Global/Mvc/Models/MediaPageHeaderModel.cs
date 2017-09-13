using _Core;
using _Core.Extensions;
using _Core.Mvc.Models;
using _Core.Helpers;
using Global.Mvc.Models;
using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;


namespace Global.Mvc.Models
{
    public class MediaPageHeaderModel : _BaseModel
    {
        /// <inheritdoc />
        public Guid ProductImage { get; set; }

        /// <inheritdoc />
        public Guid BackgroundImage { get; set; }

        /// <inheritdoc />
        public Guid LinkURLPageId { get; set; }

        /// <inheritdoc />
        public string LinkURLUrl { get; set; }

        /// <inheritdoc />
        public string LinkURLText { get; set; }

        /// <inheritdoc />
        public string PageTemplate { get; set; }

        /// <inheritdoc />
        public string LinkURLPageSelectMode { get; set; }
        /// <inheritdoc />
        public string MainContent { get; set; }


        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new MediaPageHeaderViewModel()
            {
                ProductImageMediaAsset = DynamicContentExtractor.GetMediaModel(this.ProductImage),
                BackgroundImageMediaAsset = DynamicContentExtractor.GetMediaModel(this.BackgroundImage),
                PageTemplate = this.PageTemplate,
                LinkURLLink = Utility.ConvertRelativeUrlToAbsoluteUrl(this.GetLinkedUrl(this.LinkURLPageSelectMode, this.LinkURLPageId, this.LinkURLUrl)),
                LinkURLLinkText = this.LinkURLText,
                MainContent = this.MainContent,
            };

            return viewModel;
        }

        /// <inheritdoc />
        public override bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.MainContent));
        }

        #region Private Methods

      

        #endregion
    }
}
