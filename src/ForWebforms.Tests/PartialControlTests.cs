using System;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms;
using Shouldly;
using Moq;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ForWebforms.Tests
{
    public class PartialControlTests : MvcControlTests<Partial>
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

        public void RenderWithModelViaDataSource()
        {
            var result = Host.Test((p, w) =>
            {
                var provider = new Mock<IModelProvider>();
                provider.Setup(mp => mp.ExtractModel(null)).Returns(new ModelDefinition(new DateTime(2014, 12, 18)));
                var c = new Partial(provider.Object)
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
