using _Core;
using _Core.Extensions;
using _Core.Mvc.Models;
using _Core.Helpers;
using ProductsWidgets.Mvc.Models;
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


namespace ProductsWidgets.Mvc.Models
{
    public class ProductItemModel : _BaseModel
    {
        /// <inheritdoc />
        public string Title { get; set; }
        /// <inheritdoc />
        public Guid Image { get; set; }

        /// <inheritdoc />
        public Guid DetailsLinkPageId { get; set; }

        /// <inheritdoc />
        public string DetailsLinkUrl { get; set; }

        /// <inheritdoc />
        public string DetailsLinkText { get; set; }

        /// <inheritdoc />
        public string DetailsLinkPageSelectMode { get; set; }
        /// <inheritdoc />
        public Guid IDChartLinkPageId { get; set; }

        /// <inheritdoc />
        public string IDChartLinkUrl { get; set; }

        /// <inheritdoc />
        public string IDChartLinkText { get; set; }

        /// <inheritdoc />
        public string IDChartLinkPageSelectMode { get; set; }
        /// <inheritdoc />
        public string Content { get; set; }


        /// <inheritdoc />
        public override _BaseViewModel GetViewModel()
        {
            var viewModel = new ProductItemViewModel()
            {
                Title = this.Title,
                ImageMediaAsset = DynamicContentExtractor.GetMediaModel(this.Image),
                DetailsLinkLink = Utility.ConvertRelativeUrlToAbsoluteUrl(this.GetLinkedUrl(this.DetailsLinkPageSelectMode, this.DetailsLinkPageId, this.DetailsLinkUrl)),
                DetailsLinkLinkText = this.DetailsLinkText,
                IDChartLinkLink = Utility.ConvertRelativeUrlToAbsoluteUrl(this.GetLinkedUrl(this.IDChartLinkPageSelectMode, this.IDChartLinkPageId, this.IDChartLinkUrl)),
                IDChartLinkLinkText = this.IDChartLinkText,
                Content = this.Content,
            };

            return viewModel;
        }

        /// <inheritdoc />
        public override bool IsEmpty()
        {
            return (string.IsNullOrEmpty(this.Title));
        }

        #region Private Methods

      

        #endregion
    }
}
