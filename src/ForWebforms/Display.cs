using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public class Display : MvcControl
    {
        public string TemplateName { get; set; }

        protected override void RenderMvcContent(HtmlTextWriter writer, ViewDataDictionary viewBag, ControllerContext controllerContext, ViewContext viewContext)
        {   
            var helper = new HtmlHelper(viewContext, new WebformsViewDataContainer(viewBag));
            MvcHtmlString markup;
            if (string.IsNullOrEmpty(TemplateName))
            {
                markup = helper.DisplayForModel();
            }
            else
            {
                markup = helper.DisplayForModel(TemplateName);
            }

            writer.Write(markup.ToString());
        }
    }
}