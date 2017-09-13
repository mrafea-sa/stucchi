using _Core.Mvc.Models;
using Global.Mvc.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Global.Mvc.Models
{
    /// <summary>
    /// The view model for the detail page of <see cref="MediaPageHeaderController"/>
    /// </summary>
    public class MediaPageHeaderViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the ProductImage
        /// </summary>
        public _MediaModel ProductImageMediaAsset { get; set; }

        /// <summary>
        /// Gets or sets the BackgroundImage
        /// </summary>
        public _MediaModel BackgroundImageMediaAsset { get; set; }

        /// <summary>
        /// Gets or sets the LinkURL link.
        ///</summary>
        public string LinkURLLink { get; set; }

        /// <summary>
        /// Gets or sets the LinkURL link text.
        ///</summary>
        public string LinkURLLinkText { get; set; }
        /// <summary>
        /// Gets or sets the MainContent
        /// </summary>
        public string MainContent { get; set; }

        /// <summary>
        /// Gets or sets the MainContent
        /// </summary>
        public string PageTemplate { get; set; }
    }
}