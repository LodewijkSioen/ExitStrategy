using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms
{
    [ToolboxData("<{0}:Partial runat=server />")]
    public class Partial : MvcControl
    {
        public string PartialViewName { get; set; }

        public Partial()
        {  }

        public Partial(IModelProvider provider = null, IModelValueExtractor extractor = null)
            :base(provider, extractor)
        {  }

        protected override MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag)
        {
            if (PartialViewName == null)
                throw new NullReferenceException(String.Format("The Partial View Control with ID '{0}' needs a PartialViewName.", ClientID));

            return helper.Partial(PartialViewName);
        }
    }
}