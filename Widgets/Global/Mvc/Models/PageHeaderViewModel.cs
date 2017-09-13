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
    /// The view model for the detail page of <see cref="PageHeaderController"/>
    /// </summary>
    public class PageHeaderViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the MainContent
        /// </summary>
        public string MainContent { get; set; }

    }
}