﻿using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using TestWebsite.Models;

namespace TestWebsite.Webforms.ModelBinding
{
    public partial class Edit : System.Web.UI.Page
    {
        private Person _postBackPerson;

        public Person GetModel([RouteData]int id)
        {
            return _postBackPerson ?? Person.GetBeatles().FirstOrDefault(p => p.Id == id);
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            var person = new Person();
            if (TryUpdateModel(person, new FormValueProvider(ModelBindingExecutionContext)))
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            }
            _postBackPerson = person;
        }
    }
}