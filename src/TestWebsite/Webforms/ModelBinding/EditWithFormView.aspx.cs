using ExitStrategy.TestWebsite.Models;
using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class EditWithFormView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Person _validatedPerson;

        public Person GetModel([RouteData]int id = 0)
        {
            if (id == 0)
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
                return null;
            }

            return _validatedPerson ?? Person.GetBeatles().FirstOrDefault(p => p.Id == id);
        }

        public void SetModel(Person person)
        {
            if (ModelState.IsValid)
            {
                FormView.Visible = false;
                ValidationSummary.Visible = false;
                ResultPanel.Visible = true;
                ResultDisplay.DataSource = person;
                ResultDisplay.DataBind();
                return;
            }

            ValidationSummary.Visible = true;
            _validatedPerson = person;
            FormView.DataBind();
        }
    }
}