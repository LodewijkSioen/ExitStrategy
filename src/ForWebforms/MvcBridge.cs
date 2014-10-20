using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public static class MvcBridge
    {
        public static ControllerContext CreateControllerContext(this HttpContext context, Page page)
        {
            if (context.Items["ExitStrategy_ControllerContext"] == null)
            {
                var controller = new WebformsController(context, page);
                page.RouteData.Values["controller"] = "webforms";
                context.Items["ExitStrategy_ControllerContext"] = new ControllerContext(new HttpContextWrapper(context), page.RouteData, controller);
            }
            return context.Items["ExitStrategy_ControllerContext"] as ControllerContext;
        }

        public static ViewContext CreateViewContext(this ControllerContext controllerContext, HtmlTextWriter output, ViewDataDictionary viewBag)
        {
            var view = new RazorView(controllerContext, "~/View", "", false, new string[0]);
            return new ViewContext(controllerContext, view, viewBag, new TempDataDictionary(), output);
        }
    }
}