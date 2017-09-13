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
    /// The view model for the detail page of <see cref="ProblemsController"/>
    /// </summary>
    public class ProblemsViewModel : _BaseViewModel
    {

        /// <summary>
        /// Gets or sets the selected items.
        ///</summary>
        public ArrayList ItemsSelectedItems { get; set; }
    }
}