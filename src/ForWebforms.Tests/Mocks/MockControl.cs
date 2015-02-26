using System.Web.Mvc;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms.Tests.Mocks
{
    public class MockControl : MvcControl
    {
        public MockControl(IBindingStrategySelector selector = null, IModelValueExtractor extractor = null)
            : base(selector, extractor)
        {
            
        }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            return MvcHtmlString.Create("test");
        }
    }
}
