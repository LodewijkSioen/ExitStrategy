using System;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.Controls
{   
    [Serializable]
    public class PartialControlTests : MvcControlTests
    {
        protected override MvcControl CreateControl()
        {
            return new Partial
            {
                PartialViewName = "TestWithModel"
            };
        }

        protected override string ExpectedContentForDateTime
        {
            get { return "Today is 18/12/2014"; }
        }

        protected override string ExpectedContentForDateTimeNull
        {
            get { return "Today is null"; }
        }
        
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
    }
}
