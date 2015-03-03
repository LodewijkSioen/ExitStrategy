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
            
        }

        protected void ListItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "initinsert":
                    List.InsertItemPosition = InsertItemPosition.LastItem;
                    List.EditIndex = -1;
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
            ShowResult(person);
        }

        public void InsertPerson(PersonListItem person)
        {
            ShowResult(person);
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