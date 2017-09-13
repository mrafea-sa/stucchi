using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Api.Products
{
    public class FilterModel
    {
        public ICollection<dynamic> Applications { get; set; }
        public ICollection<dynamic> BodySizes { get; set; }
        public ICollection<dynamic> Connections { get; set; }
        public ICollection<dynamic> Materials { get; set; }
        public ICollection<dynamic> WorkingPressures { get; set; }
    }
}