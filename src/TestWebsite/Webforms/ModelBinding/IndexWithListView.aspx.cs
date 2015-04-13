using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using ExitStrategy.TestWebsite.Models;
using ExitStrategy.TestWebsite.Helpers;
using System.Web.UI.WebControls;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class IndexWithListView : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ValidationSummary.Visible = false;
            if (Request.QueryString.GetValueOrEmptyString("Mode").Equals("insert", StringComparison.InvariantCultureIgnoreCase))
            {
                List.InsertItemPosition = InsertItemPosition.LastItem;
            }
        }

        public IEnumerable<PersonListItem> GetPersons()
        {
            return Person.GetBeatles().Select(p => new PersonListItem
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                EditLink = new Link("Edit", RouteTable.Routes.GetVirtualPath(null, "Webforms-ModelBinding-Edit", new RouteValueDictionary { { "Id", p.Id } }).VirtualPath)
            });
        }

        public void UpdatePerson(PersonListItem person)
        {
            if (ModelState.IsValid)
            {
                ShowResult(person);
                return;
            }
            ValidationSummary.Visible = true;
            List.EditItem.DataItem = person;
            List.EditItem.DataBind();
        }

        public void InsertPerson(PersonListItem person)
        {
            if (ModelState.IsValid)
            {
                ShowResult(person);
                return;
            }
            ValidationSummary.Visible = true;
            List.InsertItemPosition = InsertItemPosition.LastItem;
            List.InsertItem.DataItem = person;
            List.InsertItem.DataBind();
        }

        private void ShowResult(PersonListItem person)
        {
            ResultPanel.Visible = true;
            List.Visible = false;
            ResultDisplay.DataSource = person;
            ResultDisplay.DataBind();
        }
    }
}