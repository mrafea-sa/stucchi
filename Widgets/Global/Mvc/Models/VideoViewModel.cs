using _Core.Mvc.Models;
using GlobalWidgets.Mvc.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace GlobalWidgets.Mvc.Models
{
    /// <summary>
    /// The view model for the detail page of <see cref="VideoController"/>
    /// </summary>
    public class VideoViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the VideoID
        /// </summary>
        public string VideoID { get; set; }
        /// <summary>
        /// Gets or sets the VideoThumb
        /// </summary>
        public _MediaModel VideoThumbMediaAsset { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

    }
}