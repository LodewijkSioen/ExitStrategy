using MvcModelError = System.Web.Mvc.ModelError;
using MvcModelState = System.Web.Mvc.ModelState;
using MvcModelStateDictionary = System.Web.Mvc.ModelStateDictionary;
using MvcValueProviderResult = System.Web.Mvc.ValueProviderResult;
using WebformsModelState = System.Web.ModelBinding.ModelState;
using WebformsModelStateDictionary = System.Web.ModelBinding.ModelStateDictionary;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public static class ModelStateAdapter
    {
        public static void AdaptModelState(this MvcModelStateDictionary mvcState, WebformsModelStateDictionary webformsState)
        {
            foreach (var state in webformsState)
            {
                mvcState.Add(state.Key, state.Value.ToMvc());
            }
        }

        public static MvcModelState ToMvc(this WebformsModelState webformsState)
        {

            var mvcState = new MvcModelState();
            if(webformsState.Value != null)
            {
                mvcState.Value = new MvcValueProviderResult(webformsState.Value.RawValue, webformsState.Value.AttemptedValue, webformsState.Value.Culture);
            }
            foreach (var error in webformsState.Errors)
            {
                MvcModelError mvcError;
                if (error.Exception == null)
                {
                    mvcError = new MvcModelError(error.ErrorMessage);
                }
                else if (error.ErrorMessage == null)
                {
                    mvcError = new MvcModelError(error.Exception);
                }
                else
                {
                    mvcError = new MvcModelError(error.Exception, error.ErrorMessage);
                }
                mvcState.Errors.Add(mvcError);
            }
            return mvcState;
        }
    }
}