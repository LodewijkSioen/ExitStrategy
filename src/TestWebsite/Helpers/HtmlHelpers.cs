using System.Web.Mvc;

namespace TestWebsite.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString OnValidationError<TModel>(this HtmlHelper<TModel> htmlHelper, string propertyName, string error)
        {
            return htmlHelper.ViewData.ModelState.IsValidField(propertyName) ? null : new MvcHtmlString(error);
        }
    }
}