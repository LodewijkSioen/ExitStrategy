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

        public Editor(IBindingStrategySelector selector = null, IModelValueExtractor extractor = null)
            :base(selector, extractor)
        {  }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            return string.IsNullOrEmpty(DataField) ? helper.EditorForModel(TemplateName, AdditionalViewData) : helper.Editor(DataField, TemplateName, AdditionalViewData);
        }
    }
}