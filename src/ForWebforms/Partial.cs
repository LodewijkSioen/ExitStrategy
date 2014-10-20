using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms
{
    [DefaultProperty("Model")]
    [ToolboxData("<{0}:Partial runat=server></{0}:Partial>")]
    public class Partial : WebControl
    {
        public Expression<Func<object>> Model { get; set; }

        public string PartialViewName { get; set; }

        protected override void RenderContents(HtmlTextWriter output)
        {
            var controllerContext = HttpContext.Current.CreateControllerContext(Page);

            var viewEngineResult = ViewEngines.Engines.FindPartialView(controllerContext, PartialViewName);
            if (viewEngineResult != null)
            {
                var viewBag = new ViewDataDictionary();
                if(Model != null)
                {
                    var compiledModel = Model.Compile();
                    viewBag.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForExpression(Model);
                    viewBag.Model = compiledModel.Invoke();
                }
                var viewContext = controllerContext.CreateViewContext(output, viewBag);
                viewEngineResult.View.Render(viewContext, output);
            }
        }
    }
}