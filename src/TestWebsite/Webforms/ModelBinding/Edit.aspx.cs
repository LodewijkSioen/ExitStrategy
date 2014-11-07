using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using TestWebsite.Models;

namespace TestWebsite.Webforms.ModelBinding
{
    public partial class Edit : System.Web.UI.Page
    {
        private Person _validatedPerson;

        public Person GetModel([RouteData]int id)
        {
            //Not 100% happy about this... (otoh, it's better than viewstate...)
            return _validatedPerson ?? Person.GetBeatles().FirstOrDefault(p => p.Id == id);
        }

        public void SetModel(Person person)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            }
            _validatedPerson = person;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            var person = new Person();
            if (TryUpdateModel(person, ModelBoundEditor.GetValueProvider()))
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            }
            
            _validatedPerson = person;
        }
    }


}