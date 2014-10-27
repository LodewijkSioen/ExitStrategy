using System.Web.Mvc;
using TestWebsite.Models;

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