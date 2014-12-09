using System.Web.Mvc;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public static class MvcBridge
    {
        public static ControllerContext CreateControllerContext(Page page)
        {
            var context = page.Request.RequestContext.HttpContext;

            if (context.Items["ExitStrategy_ControllerContext"] == null)
            {
                var controller = new WebformsController(context, page);
                page.RouteData.Values["controller"] = "webforms";
                context.Items["ExitStrategy_ControllerContext"] = new ControllerContext(context, page.RouteData, controller);
            }
            return context.Items["ExitStrategy_ControllerContext"] as ControllerContext;
        }

        public static ViewContext CreateViewContext(Page page, HtmlTextWriter output, ViewDataDictionary viewBag)
        {
            var controllerContext = CreateControllerContext(page);

            var view = new RazorView(controllerContext, "~/View", "", false, new string[0]);
            return new ViewContext(controllerContext, view, viewBag, new TempDataDictionary(), output);
        }

        public static HtmlHelper CreateHtmlHelper(Page page, ViewDataDictionary viewBag, HtmlTextWriter output)
        {
            var viewContext = CreateViewContext(page, output, viewBag);
            return new HtmlHelper(viewContext, new WebformsViewDataContainer(viewBag));
        }
    }
}