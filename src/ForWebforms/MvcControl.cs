using System;
using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms
{
    public abstract class MvcControl : DataBoundControl
    {
        private object _model;
        private Type _modelType;
        private bool _isDataBound;

        public override bool EnableViewState { get{return false;} }

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
            }

            var controllerContext = HttpContext.Current.CreateControllerContext(Page);
            var viewContext = controllerContext.CreateViewContext(writer, viewBag);

            RenderMvcContent(writer, viewBag, controllerContext, viewContext);
        }

        protected abstract void RenderMvcContent(HtmlTextWriter writer, ViewDataDictionary viewBag, ControllerContext controllerContext, ViewContext viewContext);

    }
}