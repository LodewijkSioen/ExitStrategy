using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using ExitStrategy.TestWebsite.Models;
using ExitStrategy.TestWebsite.Helpers;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private Person _validatedPerson;

        public Person GetModel([RouteData]int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            
            return _validatedPerson ?? Person.GetBeatles().FirstOrDefault(p => p.Id == id);
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            var person = new Person();
            TryUpdateModel(person, ModelBoundEditor.GetValueProvider());

            if (ModelState.IsValid)
            {
                FormPanel.Visible = false;
                ValidationSummary.Visible = false;
                ResultPanel.Visible = true;
                ResultDisplay.DataSource = person;
                ResultDisplay.DataBind();
                return;
            }

            ValidationSummary.Visible = true;
            _validatedPerson = person;
        }

    }


}