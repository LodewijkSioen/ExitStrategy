using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using TestWebsite.Models;

namespace TestWebsite.Webforms.ModelBinding
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model = Person.GetBeatles().FirstOrDefault(p => p.Id == Convert.ToInt32(RouteData.Values["Id"]));
            }
        }

        public Person Model {get;set;}

        protected void Button_Click(object sender, EventArgs e)
        {
            var person = new Person();
            if (TryUpdateModel(person, new FormValueProvider(ModelBindingExecutionContext)))
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            }
            Model = person;
        }
    }
}