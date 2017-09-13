using System.Web.Security;
using System.Web.SessionState;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Events;
using Telerik.Sitefinity.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using SitefinityWebApp.Application;
using _Core;
using SitefinityWebApp.Application.Pages;
using System.Web.Http;
using System;
using System.Web.Routing;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(Bootstrapper_Initialized);
            Bootstrapper.Bootstrapped += Bootstrapper_Bootstrapped;

            AreaRegistration.RegisterAllAreas();

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleTable.VirtualPathProvider = System.Web.Hosting.HostingEnvironment.VirtualPathProvider;
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BundleTable.EnableOptimizations = true; 
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "RegisterRoutes")
            {
                SystemManager.RegisterWebService(typeof(PagesServiceCustom), "Sitefinity/Services/PagesServiceCustom.svc");
            }

            if (e.CommandName == "Bootstrapped")
            {
                RegisterRoutes(RouteTable.Routes);
            }
        }

        protected void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            FeatherActionInvokerCustom.Register();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });
        }
    }
}