using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IModelValueExtractor
    {
        IEnumerable<KeyValuePair<string, string>> ExtractValues(NameValueCollection form);
    }

    public class ModelValueExtractor : IModelValueExtractor
    {
        private readonly DataBoundControl _control;

        public ModelValueExtractor(DataBoundControl control)
        {
            _control = control;
        }

        public IEnumerable<KeyValuePair<string, string>> ExtractValues(NameValueCollection form)
        {
            if (form.AllKeys.Contains(_control.ClientID))
            {
                return new[]{ new KeyValuePair<string, string>(_control.ClientID, GetValue(_control.ClientID, form)) };
            }

            var formPrefix = _control.ClientID + ".";

            return from key in form.AllKeys
                where key.StartsWith(formPrefix)
                select new KeyValuePair<string, string>(key.Substring(formPrefix.Length), GetValue(key, form));
        }

        private string GetValue(string key, NameValueCollection form)
        {
            //Sigh: http://stackoverflow.com/questions/2697299/asp-net-mvc-why-is-html-checkbox-generating-an-additional-hidden-input
            var value = form[key];
            switch (value)
            {
                case "true,false":
                    return "true";
                case "false,true":
                    return "false";
                default:
                    return value;
            }
        }
    }
}