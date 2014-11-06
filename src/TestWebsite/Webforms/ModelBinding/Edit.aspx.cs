using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Routing;
using TestWebsite.Models;

namespace TestWebsite.Webforms.ModelBinding
{
    public partial class Edit : System.Web.UI.Page
    {
        private Person _validatedPerson;

        public Person GetModel([RouteData]int id)
        {
            //Not 100% happy about this... (otoh, it's better than viewstate...)
            return _validatedPerson ?? Person.GetBeatles().FirstOrDefault(p => p.Id == id);
        }

        public void SetModel(Person person)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            }
            _validatedPerson = person;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            //This will need refactoring...
            var orderedDic = new OrderedDictionary();
            ModelBoundEditor.ExtractValues(orderedDic);
            var nameValue = new NameValueCollection();
            foreach (var key in orderedDic.Keys)
            {
                nameValue.Add(key.ToString(), orderedDic[key].ToString());
            }
            var person = new Person();
            if (TryUpdateModel(person, new NameValueCollectionValueProvider(nameValue, CultureInfo.CurrentCulture)))
            {
                Response.Redirect(RouteTable.Routes.GetVirtualPath(null, "Webforms-Modelbinding", new RouteValueDictionary()).VirtualPath);
            }
            
            _validatedPerson = person;
        }
    }


}