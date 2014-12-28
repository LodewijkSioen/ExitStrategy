using ExitStrategy.ForWebforms;

namespace ForWebforms.Tests.Mocks
{
    public class MockControl : MvcControl
    {
        protected override System.Web.Mvc.MvcHtmlString RenderMvcContent(System.Web.Mvc.HtmlHelper helper, System.Web.Mvc.ViewDataDictionary viewBag)
        {
            return System.Web.Mvc.MvcHtmlString.Create("test");
        }
    }
}
