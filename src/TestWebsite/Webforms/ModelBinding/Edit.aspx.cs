using System;
using System.Linq;
using System.Web.ModelBinding;
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
                OnSucces(person);
            }
            _validatedPerson = person;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            var person = new Person();
            if (TryUpdateModel(person, ModelBoundEditor.GetValueProvider()))
            {
                OnSucces(person);
            }
            
            _validatedPerson = person;
        }

        private void OnSucces(Person person)
        {
            //Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            FormPanel.Visible = false;
            ResultPanel.Visible = true;
            ResultDisplay.DataSource = person;
            ResultDisplay.DataBind();
        }
    }


}