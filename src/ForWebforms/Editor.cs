using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public class Editor : MvcControl
    {
        public string TemplateName { get; set; }

        public Expression<Func<object>> AdditionalViewData { get; set; }

        protected override void RenderMvcContent(HtmlTextWriter writer, ViewDataDictionary viewBag, ControllerContext controllerContext, ViewContext viewContext)
        {
            if (viewBag.Model == null)
                return;

            var helper = new HtmlHelper(viewContext, new WebformsViewDataContainer(viewBag));
            MvcHtmlString markup;
            Object additionalData = null;

            if (AdditionalViewData != null)
            {
                additionalData = AdditionalViewData.Compile().Invoke();
            }
            
            if (string.IsNullOrEmpty(TemplateName))
            {
                markup = helper.EditorForModel(additionalData);
            }
            else
            {
                markup = helper.EditorForModel(TemplateName, additionalData);
            }

            writer.Write(markup.ToString());
        }
    }
}