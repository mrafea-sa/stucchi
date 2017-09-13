using _Core.Mvc.Models;
using FormsWidgets.Mvc.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace FormsWidgets.Mvc.Models
{
    /// <summary>
    /// The view model for the detail page of <see cref="ContactUsController"/>
    /// </summary>
    public class ContactUsViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }
    }
}