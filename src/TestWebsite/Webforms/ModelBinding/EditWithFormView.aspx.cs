﻿using ExitStrategy.TestWebsite.Models;
using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace ExitStrategy.TestWebsite.Webforms.ModelBinding
{
    public partial class EditWithFormView : System.Web.UI.Page
    {
        protected void FormViewInit(object sender, EventArgs e)
        {
            if (RouteData.Values["id"] == null)
            {
                FormView.DefaultMode = FormViewMode.Insert;
                FormView.InsertItemTemplate = FormView.EditItemTemplate;
                LinkNormal.NavigateUrl = GetRouteUrl("Webforms-Modelbinding-insert", new RouteValueDictionary());
                LinkFormView.NavigateUrl = GetRouteUrl("Webforms-Modelbinding-insert-formview", new RouteValueDictionary());
            }
        }

        protected void FormViewModeDataBound(object sender, EventArgs e)
        {
            if (FormView.DefaultMode == FormViewMode.Insert)
            {
                var button = FormView.FindControl("SubmitButton") as Button;
                button.Text = "Insert";
                button.CommandName = "Insert";
            }
        }

        private Person _validatedPerson;

        public Person GetPerson([RouteData]int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            return _validatedPerson ?? Person.GetBeatles().FirstOrDefault(p => p.Id == id.Value);
        }

        public void UpdatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                ShowResult(person);
                return;
            }

            ValidationSummary.Visible = true;
            _validatedPerson = person;
            FormView.DataBind();
        }

        public void InsertPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                ShowResult(person);
                return;
            }
            ValidationSummary.Visible = true;
            _validatedPerson = person;
            FormView.DataBind();
        }

        private void ShowResult(Person person)
        {
            FormView.Visible = false;
            ValidationSummary.Visible = false;
            ResultPanel.Visible = true;
            ResultDisplay.DataSource = person;
            ResultDisplay.DataBind();
        }
    }
}