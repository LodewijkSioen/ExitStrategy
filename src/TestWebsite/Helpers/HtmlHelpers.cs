using System.Web.Mvc;

namespace ExitStrategy.TestWebsite.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString OnValidationError<TModel>(this HtmlHelper<TModel> htmlHelper, string propertyName, string error)
        {
            var htmlElementName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName);
            return htmlHelper.ViewData.ModelState.IsValidField(htmlElementName) ? null : new MvcHtmlString(error);
        }        
    }
}