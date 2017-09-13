using _Core.Mvc.Models;
using ProductsWidgets.Mvc.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace ProductsWidgets.Mvc.Models
{
    /// <summary>
    /// The view model for the detail page of <see cref="ProductItemController"/>
    /// </summary>
    public class ProductItemViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Image
        /// </summary>
        public _MediaModel ImageMediaAsset { get; set; }

        /// <summary>
        /// Gets or sets the DetailsLink link.
        ///</summary>
        public string DetailsLinkLink { get; set; }

        /// <summary>
        /// Gets or sets the DetailsLink link text.
        ///</summary>
        public string DetailsLinkLinkText { get; set; }
        /// <summary>
        /// Gets or sets the IDChartLink link.
        ///</summary>
        public string IDChartLinkLink { get; set; }

        /// <summary>
        /// Gets or sets the IDChartLink link text.
        ///</summary>
        public string IDChartLinkLinkText { get; set; }
        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        public string Content { get; set; }

    }
}