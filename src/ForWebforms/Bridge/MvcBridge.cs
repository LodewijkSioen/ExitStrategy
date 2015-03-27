using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace ExitStrategy.ForWebforms.Bridge
{
    public static class MvcBridge
    {
        public static ControllerContext CreateControllerContext(RequestContext context)
        {
            if (context.HttpContext.Items["ExitStrategy_ControllerContext"] == null)
            {
                var controller = new WebformsController(context);
                context.RouteData.Values["controller"] = "webforms";
                context.HttpContext.Items["ExitStrategy_ControllerContext"] = new ControllerContext(context.HttpContext, context.RouteData, controller);
            }
            return context.HttpContext.Items["ExitStrategy_ControllerContext"] as ControllerContext;
        }

        public static ViewContext CreateViewContext(ControllerContext controllerContext, HtmlTextWriter output, ViewDataDictionary viewData)
        {
            var view = new RazorView(controllerContext, "~/View", "", false, new string[0]);
            return new ViewContext(controllerContext, view, viewData, new TempDataDictionary(), output);
        }

        public static HtmlHelper CreateHtmlHelper(RequestContext context, ViewDataDictionary viewData, HtmlTextWriter output)
        {
            var controllerContext = CreateControllerContext(context);
            var viewContext = CreateViewContext(controllerContext, output, viewData);
            return new HtmlHelper(viewContext, new WebformsViewDataContainer(viewData));
        }
    }
}