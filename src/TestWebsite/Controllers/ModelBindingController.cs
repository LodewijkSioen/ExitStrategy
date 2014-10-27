using System.Web.Mvc;
using TestWebsite.Models;

namespace TestWebsite.Controllers
{
    public class ModelBindingController : Controller
    {
        public ActionResult Index()
        {
            return View(Person.GetBeatles());
        }

        public ActionResult Edit(int id)
        {
            return View(id);
        }
    }
}