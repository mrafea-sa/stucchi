using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace _Core.Helpers
{
    public static class Utility
    {
        public static bool DatesIntervalIncludeMonth(int month, DateTime startDate, DateTime endDate)
        {
            if (startDate.Year - endDate.Year > 1)
                return true;

            if (startDate.Year == endDate.Year)
                return month >= startDate.Month && month <= endDate.Month;

            if (month >= startDate.Month || month <= endDate.Month)
                return true;

            return false;
        }


        public static string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
            if (relativeUrl.Contains("http") || relativeUrl.Contains("www.")) return relativeUrl;
            if (HttpContext.Current.Request.Url.Port != 80)
                return string.Format("http{0}://{1}:{2}{3}",
                (HttpContext.Current.Request.IsSecureConnection) ? "s" : "",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port,
                VirtualPathUtility.ToAbsolute(relativeUrl)
            );
            else
                return string.Format("http{0}://{1}{2}",
                (HttpContext.Current.Request.IsSecureConnection) ? "s" : "",
                HttpContext.Current.Request.Url.Host,
                VirtualPathUtility.ToAbsolute(relativeUrl)
            );
        }
    }
}
