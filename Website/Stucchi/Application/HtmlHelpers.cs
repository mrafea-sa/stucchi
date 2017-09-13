using _Core.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using _Core.Extensions;

namespace SitefinityWebApp.Application
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString EmbedCss(this HtmlHelper htmlHelper, string path)
        {
            var regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);
            string fileUrl = regex.Matches(System.Web.Optimization.Styles.Render(path).ToHtmlString()).OfType<Match>().Select(m => m.Groups["href"].Value).FirstOrDefault();

            string absoluteUrl = Utility.ConvertRelativeUrlToAbsoluteUrl(fileUrl);
            // take a path that starts with "~" and map it to the filesystem.
            try
            {
                var webRequest = WebRequest.Create(absoluteUrl);

                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    var strContent = reader.ReadToEnd();

                    var styleElement = new TagBuilder("style");
                    styleElement.SetInnerText(strContent);
                    return MvcHtmlString.Create(styleElement.ToString());
                }

            }
            catch (Exception ex)
            {
                // return nothing if we can't read the file for any reason
                return null;
            }
        }

        public static MvcHtmlString PreRenderedCSS(this HtmlHelper htmlHelper)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            if (url.IndexOf("?") >= 0) url = url.Substring(0, url.IndexOf("?"));
            if (url.LastIndexOf("/") == url.Length - 1) url = url.Substring(0, url.LastIndexOf("/"));

            string cssContent = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PreRenderedFilesLocation"]))
                {
                    string mappedFilePath = string.Concat(ConfigurationManager.AppSettings["PreRenderedFilesLocation"], url.Base64Encode() + ".css").Replace("=", ""); 

                    if (File.Exists(HttpContext.Current.Server.MapPath(mappedFilePath)))
                    {
                        cssContent = File.ReadAllText(HttpContext.Current.Server.MapPath(mappedFilePath));
                    }
                    else cssContent = HttpContext.Current.Server.MapPath(mappedFilePath) + " " + url;
                }
            }
            catch (Exception)
            {
            }

            var styleElement = new TagBuilder("style");
            styleElement.SetInnerText(cssContent);
            return MvcHtmlString.Create(styleElement.ToString());
        }

        public static MvcHtmlString RenderSchemaOrg(this HtmlHelper htmlHelper, string fileName)
        {
            if (fileName.Contains("gtm") &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent) &&
                    HttpContext.Current.Request.UserAgent.Contains("Speed Insights"))
                return MvcHtmlString.Create(string.Empty);

            string content = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SchemaOrgFilesLocation"]))
                {
                    string mappedFilePath = string.Concat(ConfigurationManager.AppSettings["SchemaOrgFilesLocation"], fileName + ".sch");

                    if (File.Exists(HttpContext.Current.Server.MapPath(mappedFilePath)))
                    {
                        content = File.ReadAllText(HttpContext.Current.Server.MapPath(mappedFilePath));
                    }
                }
            }
            catch (Exception)
            {
            }

            return MvcHtmlString.Create(content);
        }

        public static MvcHtmlString RenderCacheControl(this HtmlHelper htmlHelper)
        {
            string content = string.Empty;

            try
            {
                content = DateTime.Now.ToLongTimeString();
            }
            catch (Exception)
            {
            }

            return MvcHtmlString.Create(content);
        }
    }
}