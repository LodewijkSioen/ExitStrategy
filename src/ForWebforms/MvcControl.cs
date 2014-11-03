using System;
using System.Collections;
using System.Linq;
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

            if (string.IsNullOrWhiteSpace(SelectMethod))
            {
                _model = data;
                _modelType = data != null ? data.GetType() : null;
            }
            else
            {
                _modelType = Page.GetType().GetMethod(SelectMethod).ReturnType;
                if (typeof(IEnumerable).IsAssignableFrom(_modelType))
                {
                    _model = data;
                }
                else
                {
                    _model = data.OfType<object>().First();
                }
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