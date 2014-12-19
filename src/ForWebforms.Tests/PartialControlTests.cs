using System;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms;
using Shouldly;

namespace ForWebforms.Tests
{
    public class PartialControlTests : MvcControlTests<Partial>
    {
        public void RenderWithoutModel()
        {
            var result = Host.Test(() => new Partial(), (c, p) => c.PartialViewName = "Test");

            result.ShouldBe("this is a partial view!");
        }

        public void RenderWithoutViewNameShouldFail()
        {
            var ex = Host.Throws<NullReferenceException>(() => new Partial(), (c, p) =>
            {
                c.ID = "TestControl";
            });

            ex.Message.ShouldContain("The Partial View Control with ID 'TestControl' needs a PartialViewName.");
        }

        public void RenderWithModelViaDataSource()
        {
            var result = Host.Test(() => new Partial(), (c, p) =>
            {
                c.PartialViewName = "TestWithModel";
                c.DataSource = new DateTime(2014, 12, 18);
                p.DataBind();
            });

            result.ShouldBe("Today is 18/12/2014");
        }

        public void RenderWithModelViaModelbinding()
        {
            var result = Host.Test(() => new Partial(), (c, p) =>
            {
                c.PartialViewName = "TestWithModel";
                c.SelectMethod = "GetModel";
                p.DataBind();
            });

            result.ShouldBe("Today is 18/12/2014");
        }

        public void RenderWithModelViaDateSourceId()
        {
            var result = Host.Test(() => new Partial(), (c, p) =>
            {
                c.PartialViewName = "TestWithModel";
                c.DataSourceID = "ModelSource";
                c.ItemType = "System.DateTime";

                var modelSource = new ObjectDataSource("ForWebforms.Tests.MockPage", "GetModel")
                {
                    ID = "ModelSource",
                };

                c.Page.Controls.Add(modelSource);
                p.DataBind();
            });

            result.ShouldBe("Today is 18/12/2014");
        }
    }
}
