using System.Web.Mvc;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms.Tests.Mocks
{
    public class MockControl : MvcControl
    {
        public MockControl(IModelProvider provider = null, IModelValueExtractor extractor = null)
            : base(provider, extractor)
        {
            
        }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            return MvcHtmlString.Create("test");
        }
    }
}
