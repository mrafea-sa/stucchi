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
    /// The view model for the detail page of <see cref="TopNavigationController"/>
    /// </summary>
    public class TopNavigationViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the MainNavigationItems
        /// </summary>
        public ArrayList MainNavigationItems { get; set; }
    }
}