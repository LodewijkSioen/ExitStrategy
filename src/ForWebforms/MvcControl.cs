using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms
{
    public abstract class MvcControl : WebControl
    {
        public Expression<Func<object>> Model { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            var viewBag = new ViewDataDictionary();
            if (Model != null)
            {
                var compiledModel = Model.Compile();
                viewBag.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForExpression(Model);
                viewBag.Model = compiledModel.Invoke();
                viewBag.ModelState.AdaptModelState(Page.ModelState);
            }

            var controllerContext = HttpContext.Current.CreateControllerContext(Page);
            var viewContext = controllerContext.CreateViewContext(writer, viewBag);

            RenderMvcContent(writer, viewBag, controllerContext, viewContext);
        }

        protected abstract void RenderMvcContent(HtmlTextWriter writer, ViewDataDictionary viewBag, ControllerContext controllerContext, ViewContext viewContext);

    }

    public static class ModelStateAdapter
    {
        public static void AdaptModelState(this ModelStateDictionary mvcState, System.Web.ModelBinding.ModelStateDictionary webformsState)
        {
            foreach (var state in webformsState)
            {
                mvcState.Add(state.Key, state.Value.ToMvc());
            }
        }

        public static ModelState ToMvc(this System.Web.ModelBinding.ModelState webformsState)
        {
            var mvcState = new ModelState
            {
                Value = new ValueProviderResult(webformsState.Value.RawValue, webformsState.Value.AttemptedValue, webformsState.Value.Culture)
            };
            foreach (var error in webformsState.Errors)
            {
                ModelError mvcError;
                if (error.Exception == null)
                {
                    mvcError = new ModelError(error.ErrorMessage);
                }
                else if (error.ErrorMessage == null)
                {
                    mvcError = new ModelError(error.Exception);
                }
                else
                {
                    mvcError = new ModelError(error.Exception, error.ErrorMessage);
                }
                mvcState.Errors.Add(mvcError);
            }
            return mvcState;
        }
    }
}