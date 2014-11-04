using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public class Display : MvcControl
    {
        public string TemplateName { get; set; }

        public object AdditionalViewData { get; set; }

        protected override void RenderMvcContent(HtmlTextWriter writer, ViewDataDictionary viewBag, ControllerContext controllerContext, ViewContext viewContext)
        {
            if (viewBag.Model == null)
                return;

            var helper = new HtmlHelper(viewContext, new WebformsViewDataContainer(viewBag));
            MvcHtmlString markup;
            if (string.IsNullOrEmpty(TemplateName))
            {
                markup = helper.DisplayForModel(AdditionalViewData);
            }
            else
            {
                markup = helper.DisplayForModel(TemplateName, AdditionalViewData);
            }

            writer.Write(markup.ToString());
        }
    }
}