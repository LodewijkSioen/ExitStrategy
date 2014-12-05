using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.ModelBinding;
using System.Web.Mvc;
using IModelBinder = ExitStrategy.ForWebforms.ModelBinding.IModelBinder;
using MvcModelBindingContext = System.Web.Mvc.ModelBindingContext;
using WebformsModelBindingContext = System.Web.ModelBinding.ModelBindingContext;

namespace TestWebsite.Helpers
{
    public class DateTimeBinder : IModelBinder
    {
        public static Regex FormatRegex = new Regex("{.*:(.*)}", RegexOptions.Singleline);

        public object BindModel(ControllerContext controllerContext, MvcModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var format = FormatRegex.Match(bindingContext.ModelMetadata.EditFormatString);

            return BindModelImpl(value.AttemptedValue, format, value.Culture) ?? value.ConvertTo(typeof(DateTime));
        }

        public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, WebformsModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var format = FormatRegex.Match(bindingContext.ModelMetadata.EditFormatString);

            bindingContext.Model = BindModelImpl(value.AttemptedValue, format, value.Culture);

            return bindingContext.Model != null;
        }

        private static DateTime? BindModelImpl(string value, Match format, CultureInfo culture)
        {
            DateTime result;
            return (format.Success &&
                    DateTime.TryParseExact(value, format.Groups[1].Value, culture, DateTimeStyles.None, out result))
                ? (DateTime?) result
                : null;
        }
    }
}