using ExitStrategy.ForWebforms;
using Moq;
using Shouldly;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

namespace ForWebforms.Tests
{
    public class PartialControlTests
    {
        private Partial _control;

        public PartialControlTests()
        {
            var httpContext = new Mock<HttpContextBase>();
            var page = new MockPage();

            httpContext.Setup(c => c.Items).Returns(new OrderedDictionary());
            _control = new Partial();       
            //page.SetupGet(p => p.Request.RequestContext).Returns(new RequestContext(httpContext.Object, new RouteData()));
            page.Controls.Add(_control);     
        }

        public void TestRender()
        {
            var builder = new StringBuilder();
            using(var writer = new HtmlTextWriter(new StringWriter(builder)))
            {
                _control.RenderControl(writer);
            }

            builder.ToString().ShouldNotBe(null);
        }
    }

    public class MockPage : Page
    {
        public new HttpRequest Request
        {
            get { return new HttpRequest("", "", ""); }
        }
    }
}
