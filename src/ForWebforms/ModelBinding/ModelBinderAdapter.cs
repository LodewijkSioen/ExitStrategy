using System;
using MvcModelBinders = System.Web.Mvc.ModelBinders;
using WebformsModelBinderProviders = System.Web.ModelBinding.ModelBinderProviders;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public static class ModelBinderAdapter
    {
        public static void AddModelBinder(Type modelType, IModelBinder modelBinder)
        {
            MvcModelBinders.Binders.Add(modelType, modelBinder);
            WebformsModelBinderProviders.Providers.RegisterBinderForType(modelType, modelBinder);
        }
    }
}
