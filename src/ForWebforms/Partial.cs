using System.ComponentModel;
using System.Web.Mvc;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    [DefaultProperty("Model")]
    [ToolboxData("<{0}:Partial runat=server></{0}:Partial>")]
    public class Partial : MvcControl
    {
        public string PartialViewName { get; set; }

        protected override void RenderMvcContent(HtmlTextWriter writer, ViewDataDictionary viewBag, ControllerContext controllerContext, ViewContext viewContext)
        {
            var viewEngineResult = ViewEngines.Engines.FindPartialView(controllerContext, PartialViewName);
            if (viewEngineResult != null)
            {   
                viewEngineResult.View.Render(viewContext, writer);
            }
        }

    }
}