using System.Linq;
using System.Web.ModelBinding;
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var person = Person.GetBeatles().FirstOrDefault(p => p.Id == id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit([RouteData]int id, Person editPerson)
        {
            if (TryValidateModel(editPerson))
            {
                return RedirectToAction("index");    
            }

            return View(editPerson);
        }
    }
}