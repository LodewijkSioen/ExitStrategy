using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using System.Web.UI;
using ExitStrategy.TestWebsite.Models;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class Edit : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (RouteData.Values["id"] == null)
            {
                LinkNormal.NavigateUrl = GetRouteUrl("Webforms-Modelbinding-insert", new RouteValueDictionary());
                LinkFormView.NavigateUrl = GetRouteUrl("Webforms-Modelbinding-insert-formview", new RouteValueDictionary());
                SubmitButton.Text = "Insert";
            }
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