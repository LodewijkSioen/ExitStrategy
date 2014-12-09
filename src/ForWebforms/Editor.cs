using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    [ToolboxData("<{0}:Editor runat=server />")]
    public class Editor : MvcControl
    {
        public string TemplateName { get; set; }

        public object AdditionalViewData { get; set; }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            return helper.EditorForModel(TemplateName, AdditionalViewData);
        }
    }
}