using System.Web.Mvc;

namespace ExitStrategy.ForWebforms.Tests.Mocks
{
    public class MockControl : MvcControl
    {
        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            return MvcHtmlString.Create("test");
        }
    }
}
