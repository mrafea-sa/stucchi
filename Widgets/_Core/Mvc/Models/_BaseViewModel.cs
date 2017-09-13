using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace _Core.Mvc.Models
{
    public class _BaseViewModel : ContentDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        public ArrayList SelectedItems { get; set; }
    }
}
