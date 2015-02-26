using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms
{
    [ToolboxData("<{0}:Display runat=server />")]
    public class Display : MvcControl
    {
        public Display()
        { }

        public Display(IBindingStrategySelector selector = null, IModelValueExtractor extractor = null)
            :base(selector, extractor)
        {  }

        public string TemplateName { get; set; }

        public object AdditionalViewData { get; set; }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {   
            return string.IsNullOrEmpty(DataField) ? helper.DisplayForModel(TemplateName, AdditionalViewData) : helper.Display(DataField, TemplateName, AdditionalViewData);
        }
    }
}