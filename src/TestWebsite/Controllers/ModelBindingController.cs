using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;
using ExitStrategy.TestWebsite.Models;

namespace ExitStrategy.TestWebsite.Controllers
{
    public class ModelBindingController : Controller
    {
        public ActionResult Index()
        {
            return View(Person.GetBeatles().Select(p => new PersonListItem
            {
                FirstName = p.FirstName, 
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                EditLink = new Link("Edit", Url.Action("Edit", new {p.Id}))
            }));
        }

        public RedirectResult ListView()
        {
            //hack because the webforms side has multiple routes
            return Redirect(Url.Action("Index")); 
        }

        [HttpGet]
        public ActionResult Edit(int id, string mode)
        {
            //hack because the webforms side has multiple routes
            if (!string.IsNullOrEmpty(mode))
            {
                return Redirect(Url.Action("Edit", new {id}));
            }

            var person = Person.GetBeatles().FirstOrDefault(p => p.Id == id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit([RouteData]int id, Person editPerson)
        {
            if(ModelState.IsValid)
            {
                return View("Post", editPerson);
            }

            return View(editPerson);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Insert(Person editPerson)
        {
            if (ModelState.IsValid)
            {
                return View("Post", editPerson);
            }

            return View("Edit", editPerson);
        }
    }
}