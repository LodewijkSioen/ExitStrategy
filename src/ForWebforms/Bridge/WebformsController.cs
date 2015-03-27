using System.Web.Mvc;
using System.Web.Routing;

namespace ExitStrategy.ForWebforms.Bridge
{
    internal class WebformsController : Controller
    {
        public WebformsController(RequestContext context)
        {
            this.Initialize(context);
        }
    }
    
}