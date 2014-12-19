using System;
using ExitStrategy.ForWebforms;
using Shouldly;

namespace ForWebforms.Tests
{
    public class DisplayControlTests : MvcControlTests<Display>
    {
        public void RenderWithoutTemplateNameShouldRenderDefaultTemplate()
        {
            var result = Host.Test(() => new Display(), (c, p) =>
            {
                c.DataSource = new DateTime(2014, 12, 18);
                c.DataBind();
            });
            result.ShouldBe("This is a displaytemplate for 18/12/2014");
        }
    }
}