using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace ExitStrategy.ForWebforms
{
    public class WebformsController : Controller
    {
        public WebformsController(HttpContext context, Page page)
        {
            this.Initialize(new RequestContext(new HttpContextWrapper(context), page.RouteData));
        }
    }
    
}