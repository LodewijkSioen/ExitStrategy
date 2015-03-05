using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using ExitStrategy.TestWebsite.Models;
using System.Web.UI.WebControls;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class IndexWithListView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSummary.Visible = false;
        }

        protected void ListItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "initinsert":
                    List.InsertItemPosition = InsertItemPosition.LastItem;
                    List.EditIndex = -1;
                    e.Handled = true;
                    break;
                case "cancelinsert":
                    List.InsertItemPosition = InsertItemPosition.None;
                    List.EditIndex = -1;
                    e.Handled = true;
                    break;
                case "edit":
                    List.InsertItemPosition = InsertItemPosition.None;
                    e.Handled = false;
                    break;
                default:
                    e.Handled = false;
                    break;
            }
            
        }

        public IEnumerable<PersonListItem> GetPersons()
        {
            return Person.GetBeatles().Select(p => new PersonListItem
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
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