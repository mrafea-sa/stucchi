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
    /// The view model for the detail page of <see cref="ProductSearchController"/>
    /// </summary>
    public class ProductSearchViewModel : _BaseViewModel
    {
        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }
    }
}