﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using ExitStrategy.TestWebsite.Models;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class IndexWithListView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

        }
    }
}