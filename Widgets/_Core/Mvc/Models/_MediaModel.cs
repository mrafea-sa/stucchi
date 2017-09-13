using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Mvc.Models
{
    public class _MediaModel
    {
        /// <summary>
        /// Gets or sets the image title.
        /// </summary>
        public string ImageTitle { get; set; }

        /// <summary>
        /// Gets or sets the image alternative text.
        /// </summary>
        public string ImageAlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the selected size image URL.
        /// </summary>
        public string ImageSelectedSizeUrl { get; set; }
    }
}
