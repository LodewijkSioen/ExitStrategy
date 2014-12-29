using System;
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
                var modelProvider = new Mock<IModelProvider> ();
                modelProvider.Setup(m => m.ExtractModel(null)).Returns(new ModelDefinition(new DateTime(2014, 12, 18)));
                var c = new Editor(modelProvider.Object);
                c.DataBind();
                p.Controls.Add(c);

                c.RenderControl(w);
            });

            result.ShouldBe("This is an editortemplate for 18/12/2014");
        }
    }
}