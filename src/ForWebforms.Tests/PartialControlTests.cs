using System;
using ExitStrategy.ForWebforms.ModelBinding;
using Moq;
using Shouldly;
using System.Web.Mvc;
using System.Collections;

namespace ExitStrategy.ForWebforms.Tests
{
    public class PartialControlTests : MvcControlTests
    {
        public void RenderWithoutModel()
        {
            var result = Host.Test((p, w) =>
            {
                var c = new Partial()
                {
                    PartialViewName = "Test"
                };
                p.Controls.Add(c);

                c.RenderControl(w);
            });
            result.ShouldBe("this is a partial view!");
        }

        public void RenderWithoutViewNameShouldFail()
        {
            var ex = Host.Throws<NullReferenceException>((p, w) =>
            {
                var c = new Partial()
                {
                    ID = "TestControl"
                };
                p.Controls.Add(c);

                c.RenderControl(w);
            });

            ex.Message.ShouldContain("The Partial View Control with ID 'TestControl' needs a PartialViewName.");
        }

        public void RenderWithModel()
        {
            var result = Host.Test((p, w) =>
            {
                var selector = new Mock<IBindingStrategySelector>();
                var strategy = new Mock<IBindingStrategy>();
                var modelMetaData = ModelMetadata.FromLambdaExpression(x => x.Date, new ViewDataDictionary<DateTime>(new DateTime(2014, 12, 18)));
                strategy.Setup(s => s.ExtractModel(It.IsAny<IEnumerable>())).Returns(new ModelDefinition(modelMetaData, new DateTime(2014, 12, 18)));
                selector.Setup(s => s.GetStrategy(It.IsAny<MvcControl>())).Returns(strategy.Object);
                var c = new Partial(selector.Object)
                {
                    PartialViewName = "TestWithModel"
                };
                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            result.ShouldBe("Today is 18/12/2014");
        }
    }
}
