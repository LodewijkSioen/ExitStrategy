using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMvcModelBinder = System.Web.Mvc.IModelBinder;
using IWebformsModelBinder = System.Web.ModelBinding.IModelBinder;

namespace TestWebsite.Helpers
{
    public class DateTimeBinder : IMvcModelBinder, IWebformsModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var format = bindingContext.ModelMetadata.EditFormatString;

            var date = DateTime.ParseExact(value.RawValue.ToString(), format, CultureInfo.CurrentUICulture);

            return date;
        }

        public bool BindModel(System.Web.ModelBinding.ModelBindingExecutionContext modelBindingExecutionContext, System.Web.ModelBinding.ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}