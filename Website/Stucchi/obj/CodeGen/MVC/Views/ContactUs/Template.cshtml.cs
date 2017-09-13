#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/MVC/Views/ContactUs/Template.cshtml")]
    public partial class _MVC_Views_ContactUs_Template_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _MVC_Views_ContactUs_Template_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!-- contact us widget -->\r\n<section");

WriteLiteral(" class=\"main-form margin-bottom-10 margin-top-8\"");

WriteLiteral(" ng-app=\"ContactUsApp\"");

WriteLiteral(" ng-controller=\"ContactUsController\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-xs-12 col-lg-8 col-lg-offset-2\"");

WriteLiteral(" ng-if=\"!formErrors.IsSuccesssfullySent.status\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12\"");

WriteLiteral(">\r\n                        <h2>Send us a message</h2>\r\n                    </div>" +
"\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>First Name<span>*</span></label>\r\n             " +
"           <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.FirstName\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"form-control-error\"");

WriteLiteral(" ng-if=\"formErrors.FirstName.status\"");

WriteLiteral(">{{formErrors.FirstName.message}}</div>\r\n                    </div>\r\n            " +
"        <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Last Name<span>*</span></label>\r\n              " +
"          <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.LastName\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"form-control-error\"");

WriteLiteral(" ng-if=\"formErrors.LastName.status\"");

WriteLiteral(">{{formErrors.LastName.message}}</div>\r\n                    </div>\r\n             " +
"   </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Company<span>*</span></label>\r\n                " +
"        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.Company\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"form-control-error\"");

WriteLiteral(" ng-if=\"formErrors.Company.status\"");

WriteLiteral(">{{formErrors.Company.message}}</div>\r\n                    </div>\r\n              " +
"      <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Phone </label>\r\n                        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.Phone\"");

WriteLiteral(">\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Address Line 1</label>\r\n                       " +
" <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.Address\"");

WriteLiteral(">\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Address Line 2</label>\r\n                       " +
" <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.Address2\"");

WriteLiteral(">\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>City</label>\r\n                        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.City\"");

WriteLiteral(">\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>State</label>\r\n                        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.State\"");

WriteLiteral(">\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Zip Code</label>\r\n                        <inpu" +
"t");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.Zip\"");

WriteLiteral(">\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <label>Country</label>\r\n                        <input" +
"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" ng-model=\"formRequest.Country\"");

WriteLiteral(">\r\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 col-sm-push-6 checkbox column margin-bottom-3 \"");

WriteLiteral(">\r\n                        <p");

WriteLiteral(" class=\"text-uppercase\"");

WriteLiteral(">REASON FOR CONTACT:</p>\r\n                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"check1\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" ng-model=\"formRequest.ReasonsForContact.RequestForDrawing.Status\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" for=\"check1\"");

WriteLiteral(">Request for drawing</label>\r\n                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"check2\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" ng-model=\"formRequest.ReasonsForContact.RequestForCatalog.Status\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" for=\"check2\"");

WriteLiteral(">Request for catalog</label>\r\n                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"check3\"");

WriteLiteral(" name=\"\"");

WriteLiteral(" ng-model=\"formRequest.ReasonsForContact.RequestForProductSample.Status\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" for=\"check3\"");

WriteLiteral(">Request for product sample</label>\r\n                    </div>\r\n                " +
"    <div");

WriteLiteral(" class=\"col-xs-12 col-sm-6 col-sm-pull-6  column margin-bottom-5\"");

WriteLiteral(">\r\n                        <label>Comments</label>\r\n                        <text" +
"area");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" ng-model=\"formRequest.Message\"");

WriteLiteral("></textarea>\r\n                    </div>\r\n                </div>\r\n               " +
" <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 checkbox  column margin-bottom-5\"");

WriteLiteral(">\r\n                        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"check4\"");

WriteLiteral(" name=\"\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" class=\"no-margin\"");

WriteLiteral(" for=\"check4\"");

WriteLiteral(" ng-model=\"formRequest.HasOptedForSubscription\"");

WriteLiteral(">Yes, I would like to receive future communication from Stucchi USA INC.</label>\r" +
"\n                    </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"col-xs-12 column margin-bottom-1\"");

WriteLiteral(">\r\n                        <button");

WriteLiteral(" class=\"button button-white text-uppercase\"");

WriteLiteral(" ng-click=\"submitFormRequest()\"");

WriteLiteral(">send message</button>\r\n                    </div>\r\n                </div>\r\n     " +
"       </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-xs-12 col-lg-8 col-lg-offset-2 text-center succes-message\"");

WriteLiteral(" ng-if=\"formErrors.IsSuccesssfullySent.status\"");

WriteLiteral(">\r\n            <p");

WriteLiteral(" class=\"margin-bottom-2\"");

WriteLiteral(">Your message has been successfully sent.</p>\r\n            <button");

WriteLiteral(" class=\"button button-white text-uppercase\"");

WriteLiteral(" ng-click=\"newFormRequest()\"");

WriteLiteral(">new message</button>\r\n        </div>\r\n    </div>\r\n</section>\r\n<!-- end contact u" +
"s widget -->");

        }
    }
}
#pragma warning restore 1591