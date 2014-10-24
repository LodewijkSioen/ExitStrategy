using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestWebsite
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "mvc/{controller}/{action}",
                defaults: new { action= "index", controller = "mvc"}
            );

            routes.MapPageRoute("Webforms", "Webforms", "~/Webforms/Index.aspx");
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bootstrapjs").Include(
                    "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/styles")
                .IncludeDirectory("~/Content/Style/", "*.css"));
        }
    }
}