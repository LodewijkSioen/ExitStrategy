using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExitStrategy.ForWebforms;
using Shouldly;

namespace ForWebforms.Tests
{
    public class MvcBridgeTests
    {
        public void CreatingAControllerContextShouldAddItToTheRequestItems()
        {
            var contextItems = new OrderedDictionary();
            var httpContext = new Moq.Mock<HttpContextBase>();
            httpContext.Setup(c => c.Items).Returns(contextItems);
            var requestContext = new RequestContext(httpContext.Object, new RouteData());

            var context1 = MvcBridge.CreateControllerContext(requestContext);

            context1.ShouldNotBe(null);
            contextItems.Count.ShouldBe(1);
            contextItems[0].ShouldBeOfType<ControllerContext>();

            var context2 = MvcBridge.CreateControllerContext(requestContext);

            context2.ShouldNotBe(null);
            contextItems.Count.ShouldBe(1);
            contextItems[0].ShouldBeOfType<ControllerContext>();
            context2.ShouldBe(context1);
        }

        public void CreatingAViewContext()
        {
            
        }
    }
}
