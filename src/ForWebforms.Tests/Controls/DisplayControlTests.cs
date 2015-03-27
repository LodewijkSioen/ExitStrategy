using System;
using System.Collections;
using ExitStrategy.ForWebforms.ModelBinding;
using Moq;
using Shouldly;
using System.Web.Mvc;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    public class DisplayControlTests : MvcControlTests
    {
        public void RenderWithoutTemplateNameShouldRenderDefaultTemplate()
        {
            var r = Host.Test((p, w) =>
            {
                var selector = new Mock<IBindingStrategySelector>();
                var strategy = new Mock<IBindingStrategy>();
                var modelMetaData = ModelMetadata.FromLambdaExpression(x => x.Date, new ViewDataDictionary<DateTime>(new DateTime(2014, 12, 18)));
                strategy.Setup(s => s.ExtractModel(It.IsAny<IEnumerable>())).Returns(new ModelDefinition(modelMetaData, new DateTime(2014, 12, 18)));
                selector.Setup(s => s.GetStrategy(It.IsAny<MvcControl>())).Returns(strategy.Object);
                var c = new Display(selector.Object);
                c.DataBind();
                p.Controls.Add(c);

                c.RenderControl(w);
            });

            r.ShouldBe("This is a displaytemplate for 18/12/2014");
        }
    }
}