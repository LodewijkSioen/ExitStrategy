using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using TestWebsite.Models;

namespace TestWebsite.Webforms.ModelBinding
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Model = Person.GetBeatles().Select(p => new PersonListItem
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                EditLink = new Link("Edit", RouteTable.Routes.GetVirtualPath(null, "Webforms-ModelBinding-Edit", new RouteValueDictionary { { "Id", p.Id } }).VirtualPath)
            });
        }

        public IEnumerable<PersonListItem> Model { get; set; }
    }
}