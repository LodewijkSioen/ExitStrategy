using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    internal class WebformsController : Controller
    {
        public WebformsController(HttpContextBase context, Page page)
        {
            this.Initialize(new RequestContext(context, page.RouteData));
        }
    }
    
}