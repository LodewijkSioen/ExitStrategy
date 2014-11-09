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
            return View(Person.GetBeatles().Select(p => new PersonListItem
            {
                FirstName = p.FirstName, 
                LastName = p.LastName,
                EditLink = new Link("Edit", Url.Action("Edit", new {p.Id}))
            }));
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
                return View("Post", editPerson);
            }

            return View(editPerson);
        }
    }
}