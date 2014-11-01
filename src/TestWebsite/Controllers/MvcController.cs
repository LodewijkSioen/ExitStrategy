using System.Web.Mvc;

namespace TestWebsite.Controllers
{
    public class MvcController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}