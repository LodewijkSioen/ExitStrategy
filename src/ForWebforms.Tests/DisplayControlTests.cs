using System;
using ExitStrategy.ForWebforms;
using Shouldly;

namespace ForWebforms.Tests
{
    public class DisplayControlTests : MvcControlTests<Display>
    {
        public void RenderWithoutTemplateNameShouldRenderDefaultTemplate()
        {
            var r = Host.Test((p, w) =>
            {
                var c = new Display();
                c.DataSource = new DateTime(2014, 12, 18);
                c.DataBind();
                p.SetControlUnderTest(c);

                p.GetControlUnderTest<Display>().RenderControl(w);
            });

            r.ShouldBe("This is a displaytemplate for 18/12/2014");
        }
    }
}