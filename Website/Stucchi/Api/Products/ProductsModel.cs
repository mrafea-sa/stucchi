using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Api.Products
{
    public class ProductsModel
    {
        public bool HasMoreItems { get; set; }
        public ICollection<dynamic> Products { get; set; }
    }
}