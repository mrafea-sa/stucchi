using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
using SitefinityWebApp.Application.Custom_Bundles;
using System.Web.Optimization;

namespace SitefinityWebApp.Application
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.IgnoreList.Clear();


            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();

            var commonStylesBundle = new CustomStyleBundle("~/Bundles/CommonStyles");
            commonStylesBundle.Include(
                "~/Resources/Css/bootstrap.min.css",
                 "~/Resources/Css/font-awesome.min.css",
                 "~/Resources/Css/main.css",
                 "~/Resources/Fonts/stylesheet.css",
                 "~/Resources/Css/custom.css",
                 "~/Resources/Css/upload.css"
                );

            bundles.Add(commonStylesBundle);

            var commonScriptsBundle = new ScriptBundle("~/Bundles/CommonScripts");
            commonScriptsBundle.Include(
                "~/Resources/Js/bootstrap.min.js",
                "~/Resources/Js/jquery.easing.min.js",
                "~/Resources/Js/main.js",
                "~/Resources/Js/angular.min.js",
                "~/Resources/Js/angular-resource.min.js",
                "~/Resources/Js/angular-sanitize.min.js",
                "~/Resources/Js/loadDeferedJs.js",
                "~/Resources/Js/Widgets/helpdesk.js",
                "~/Resources/Js/Widgets/contactus.js",
                "~/Resources/Js/Widgets/image-submission.js",
                "~/Resources/Js/Widgets/products-search.js",
                "~/Resources/Js/Widgets/products-categories.js",
                "~/Resources/Js/Widgets/upload-file.directive.js"
                );

            bundles.Add(commonScriptsBundle);

            var commonAdminScriptsBundle = new ScriptBundle("~/Bundles/CommonAdminScripts");

            commonAdminScriptsBundle.Include(
                "~/Resources/Js/bootstrap.min.js",
                "~/Resources/Js/jquery.easing.min.js",
                "~/Resources/Js/main.js",
                "~/Resources/Js/loadDeferedJs.js",
                "~/Resources/Js/Widgets/helpdesk.js",
                "~/Resources/Js/Widgets/contactus.js",
                "~/Resources/Js/Widgets/image-submission.js",
                "~/Resources/Js/Widgets/products-search.js",
                "~/Resources/Js/Widgets/products-categories.js",
                "~/Resources/Js/Widgets/upload-file.directive.js"
                );

            bundles.Add(commonAdminScriptsBundle);

        }
    }
}