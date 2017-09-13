using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SitefinityWebApp.Application.Custom_Bundles
{
    public class CustomStyleBundle : Bundle
    {
        public CustomStyleBundle(string virtualPath)
            : base(virtualPath)
        {
            this.Builder = new CustomStyleBuilder();
        }

        public CustomStyleBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        {
            this.Builder = new CustomStyleBuilder();
        }
    }
}