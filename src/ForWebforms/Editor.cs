using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms
{
    [ToolboxData("<{0}:Editor runat=server />")]
    public class Editor : MvcControl
    {
        public string TemplateName { get; set; }

        public object AdditionalViewData { get; set; }

        public Editor()
        { }

        public Editor(IModelProvider provider = null, IModelValueExtractor extractor = null)
            :base(provider, extractor)
        {  }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            return helper.EditorForModel(TemplateName, AdditionalViewData);
        }
    }
}