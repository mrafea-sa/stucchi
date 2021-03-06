﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace SitefinityWebApp.Application.Custom_Bundles
{
    public class CustomStyleBuilder : IBundleBuilder
    {
        public virtual string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            var content = new StringBuilder();
            foreach (var file in files)
            {
                FileInfo f = new FileInfo(HttpContext.Current.Server.MapPath(file.VirtualFile.VirtualPath));
                CssSettings settings = new CssSettings();
                settings.IgnoreAllErrors = true; //this is what you want
                settings.CommentMode = Microsoft.Ajax.Utilities.CssComment.Important;
                var minifier = new Microsoft.Ajax.Utilities.Minifier();
                string readFile = Read(f);
                string res = minifier.MinifyStyleSheet(readFile, settings);
                content.Append(res);
            }

            return content.ToString();
        }

        public static string Read(FileInfo file)
        {
            using (var r = file.OpenText())
            {
                return r.ReadToEnd();
            }
        }
    }
}