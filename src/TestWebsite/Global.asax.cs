using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.TestWebsite.Helpers;

namespace ExitStrategy.TestWebsite
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);

            ModelBinderAdapter.AddModelBinder(typeof(DateTime), new DateTimeBinder());
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "mvc/{controller}/{action}/{id}/{mode}",
                defaults: new { action= "index", controller = "mvc", id = 0, mode = string.Empty}
            );

            routes.MapPageRoute("Webforms", "Webforms", "~/Webforms/Index.aspx");
            routes.MapPageRoute("Webforms-ModelBinding", "Webforms/ModelBinding", "~/Webforms/ModelBinding/Index.aspx");
            routes.MapPageRoute("Webforms-ModelBinding-ListView", "Webforms/ModelBinding/listview", "~/Webforms/ModelBinding/IndexWithListView.aspx");
            routes.MapPageRoute("Webforms-ModelBinding-Insert", "Webforms/ModelBinding/insert", "~/Webforms/Modelbinding/Edit.aspx");
            routes.MapPageRoute("Webforms-ModelBinding-Insert-FormView", "Webforms/ModelBinding/Insert/formview", "~/Webforms/ModelBinding/EditWithFormView.aspx");
            routes.MapPageRoute("Webforms-ModelBinding-Edit", "Webforms/ModelBinding/edit/{id}", "~/Webforms/ModelBinding/Edit.aspx");
            routes.MapPageRoute("Webforms-ModelBinding-Edit-FormView", "Webforms/ModelBinding/edit/{id}/formview", "~/Webforms/ModelBinding/EditWithFormView.aspx");
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/bootstrap-datepicker.js")
                .Include("~/Scripts/main.js")
            );

            bundles.Add(new StyleBundle("~/styles")
                .IncludeDirectory("~/Content/Style/", "*.css"));
        }
    }
}