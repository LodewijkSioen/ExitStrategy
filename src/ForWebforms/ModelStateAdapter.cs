using System.Web.Mvc;

namespace ExitStrategy.ForWebforms
{
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