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
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var browser = new Mock<HttpBrowserCapabilitiesBase>();
            var cookies = new HttpCookieCollection();
            var items = new ListDictionary();

            browser.Setup(b => b.IsMobileDevice).Returns(false);
            request.Setup(r => r.Cookies).Returns(cookies);
            request.Setup(r => r.RequestContext).Returns(new RequestContext(context.Object, new RouteData()));
            request.Setup(r => r.Browser).Returns(browser.Object);
            response.Setup(r => r.Cookies).Returns(cookies);
            context.Setup(ctx => ctx.Items).Returns(items);

            context.SetupGet(ctx => ctx.Request).Returns(request.Object);
            context.SetupGet(ctx => ctx.Response).Returns(response.Object);
            context.SetupGet(ctx => ctx.Session).Returns(session.Object);
            context.SetupGet(ctx => ctx.Server).Returns(server.Object);

            var page = new MockPage();
            HttpContextProvider.SetHttpContext(context.Object);

            _control = new Partial();       
            page.Controls.Add(_control);     
        }

        public void TestRender()
        {
            var builder = new StringBuilder();
            using(var writer = new HtmlTextWriter(new StringWriter(builder)))
            {
                _control.PartialViewName = "test";
                _control.RenderControl(writer);
            }

            builder.ToString().ShouldNotBe(null);
        }
    }

    public class MockPage : Page
    {
        
    }
}
