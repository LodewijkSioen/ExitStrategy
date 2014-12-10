using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms
{
    public abstract class MvcControl : DataBoundControl, IBindableControl
    {
        private object _model;
        private Type _modelType;
        private bool _isDataBound;

        public override bool EnableViewState { get{return false;} }

        protected override void ValidateDataSource(object dataSource)
        {
            //Do noting, we accept anything
        }

        protected override void PerformDataBinding(IEnumerable data)
        {
            _isDataBound = true;

            if (IsUsingModelBinders)
            {
                //If the control is databound using the 4.5 SelectMethod, we can determine the 
                //type of the model by looking at the returntype of the SelectMethod
                _modelType = Page.GetType().GetMethod(SelectMethod).ReturnType;

                //Webforms Databinding only works with IEnumerables (hello .net 1.0). It has no concept of
                //databinding to a single item. So we need to handle this here: If the type of the model 
                //is not an IEnumerable, we just need the first item in the collection.
                if (data == null)
                {
                    _model = null;
                }
                else if (typeof (IEnumerable).IsAssignableFrom(_modelType))
                {
                    _model = data;
                }
                else
                {
                    var enumerator = data.GetEnumerator();
                    enumerator.MoveNext();
                    _model = enumerator.Current;
                }
                
            }
            else if (DataSource != null)
            {
                _model = DataSource;
                _modelType = DataSource.GetType();
            }
            else
            {
                _model = data;
                _modelType = data != null ? data.GetType() : null;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var viewBag = new ViewDataDictionary();
            viewBag.ModelState.AdaptModelState(Page.ModelState);
            
            if (_isDataBound)
            {
                viewBag.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, _modelType);
                viewBag.Model = _model;
                viewBag.TemplateInfo.HtmlFieldPrefix = ClientID;
            }

            var helper = MvcBridge.CreateHtmlHelper(Page.Request.RequestContext, viewBag, writer);

            var markup = RenderMvcContent(helper, viewBag);

            writer.Write(markup.ToString());
        }

        protected abstract MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag);

        private IEnumerable<KeyValuePair<String, String>> ExtractValues()
        {
            var formPrefix = ClientID + ".";
            var form = Context.Request.Form;

            return from key in form.Keys.OfType<string>()
                   where key.StartsWith(formPrefix)
                   select new KeyValuePair<string, string>(key.Substring(formPrefix.Length), GetValue(key, form));
        }

        public void ExtractValues(IOrderedDictionary dictionary)
        {
            foreach (var value in ExtractValues())
            {
                dictionary.Add(value.Key, value.Value);
            }
        }

        public System.Web.ModelBinding.IValueProvider GetValueProvider()
        {
            var nameValueCollection = new NameValueCollection();
            foreach (var value in ExtractValues())
            {
                nameValueCollection.Add(value.Key, value.Value);
            }
            return new System.Web.ModelBinding.NameValueCollectionValueProvider(nameValueCollection, CultureInfo.CurrentCulture);
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