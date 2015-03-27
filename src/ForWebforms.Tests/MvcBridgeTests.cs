using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Moq;
using Shouldly;
using ExitStrategy.ForWebforms.Bridge;

namespace ExitStrategy.ForWebforms.Tests
{
    public class MvcBridgeTests
    {
        private readonly RequestContext _requestContext;
        private readonly OrderedDictionary _contextItems;

        public MvcBridgeTests()
        {
            _contextItems = new OrderedDictionary();
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Items).Returns(_contextItems);
            _requestContext = new RequestContext(httpContext.Object, new RouteData());
        }

        public void CreatingAControllerContextShouldAddItToTheRequestItems()
        {
            var context = MvcBridge.CreateControllerContext(_requestContext);

            context.ShouldNotBe(null);
            _contextItems.Count.ShouldBe(1);
            _contextItems[0].ShouldBeOfType<ControllerContext>();
        }

        public void CreatingASecondControllerContextShouldPickTheOneFromTheContextItems()
        {
            var context1 = MvcBridge.CreateControllerContext(_requestContext);
            var context2 = MvcBridge.CreateControllerContext(_requestContext);

            _contextItems.Count.ShouldBe(1);
            context2.ShouldBe(context1);
        }

        public void CreateAViewContext()
        {
            var controllerContext = new Mock<ControllerContext>();
            using (var writer = new HtmlTextWriter(new StringWriter(new StringBuilder())))
            {
                var viewContext = MvcBridge.CreateViewContext(controllerContext.Object, writer, new ViewDataDictionary());

                viewContext.ShouldNotBe(null);
            }
        }

        public void CreateAHtmlhelper()
        {
            using (var writer = new HtmlTextWriter(new StringWriter(new StringBuilder())))
            {
                var helper = MvcBridge.CreateHtmlHelper(_requestContext, new ViewDataDictionary(), writer);

                helper.ShouldNotBe(null);
            }
        }
    }
}
