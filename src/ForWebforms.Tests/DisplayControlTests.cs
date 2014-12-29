using System;
using ExitStrategy.ForWebforms.ModelBinding;
using Moq;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests
{
    public class DisplayControlTests : MvcControlTests
    {
        public void RenderWithoutTemplateNameShouldRenderDefaultTemplate()
        {
            var r = Host.Test((p, w) =>
            {
                var modelProvider = new Mock<IModelProvider>();
                modelProvider.Setup(m => m.ExtractModel(null)).Returns(new ModelDefinition(new DateTime(2014, 12, 18)));
                var c = new Display(modelProvider.Object);
                c.DataBind();
                p.Controls.Add(c);

                c.RenderControl(w);
            });

            r.ShouldBe("This is a displaytemplate for 18/12/2014");
        }
    }
}