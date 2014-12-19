using System;
using ExitStrategy.ForWebforms;
using Shouldly;

namespace ForWebforms.Tests
{
    public class EditorControlTests : MvcControlTests<Editor>
    {
        public void RenderWithoutTemplateNameShouldRenderDefaultTemplate()
        {
            var result = Host.Test(() => new Editor(), (c, p) =>
            {
                c.DataSource = new DateTime(2014, 12, 18);
                c.DataBind();
            });
            result.ShouldBe("This is an editortemplate for 18/12/2014");
        }
    }
}