using System;
using System.Collections;
using System.Web.Mvc;
using ExitStrategy.ForWebforms.ModelBinding;
using Moq;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests
{
    public class EditorControlTests : MvcControlTests
    {
        public void RenderWithoutTemplateNameShouldRenderDefaultTemplate()
        {
            var result = Host.Test((p, w) =>
            {
                var selector = new Mock<IBindingStrategySelector>();
                var strategy = new Mock<IBindingStrategy>();
                var modelMetaData = ModelMetadata.FromLambdaExpression(x => x.Date, new ViewDataDictionary<DateTime>(new DateTime(2014, 12, 18)));
                strategy.Setup(s => s.ExtractModel(It.IsAny<IEnumerable>())).Returns(new ModelDefinition(modelMetaData, new DateTime(2014, 12, 18)));
                selector.Setup(s => s.GetStrategy(It.IsAny<MvcControl>())).Returns(strategy.Object);
                var c = new Editor(selector.Object);
                c.DataBind();
                p.Controls.Add(c);

                c.RenderControl(w);
            });

            result.ShouldBe("This is an editortemplate for 18/12/2014");
        }
    }
}