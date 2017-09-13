using _Core.Mvc.Models;
using CalloutsWidgets.Mvc.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace CalloutsWidgets.Mvc.Models
{
    /// <summary>
    /// The view model for the detail page of <see cref="BackgroundImageCalloutController"/>
    /// </summary>
    public class BackgroundImageCalloutViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the MainContent
        /// </summary>
        public string MainContent { get; set; }
        /// <summary>
        /// Gets or sets the MainContent
        /// </summary>
        public string AlignmentOptions { get; set; }
        
        /// <summary>
        /// Gets or sets the BackgroundImage
        /// </summary>
        public _MediaModel BackgroundImageMediaAsset { get; set; }

    }
}