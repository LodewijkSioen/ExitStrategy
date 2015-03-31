using System;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    [Serializable]
    public abstract class MvcControlTests
    {
        protected readonly WebformsScaffold Host;

        protected MvcControlTests()
        {
            Host = WebformsScaffold.Create();
        }

        protected abstract MvcControl CreateControl();
        protected abstract String ExpectedContentForDateTime { get; }
        protected abstract String ExpectedContentForDateTimeNull { get; }

        public void ModelBindingForDataFieldShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var c = CreateControl();
                c.SelectMethod = "GetModel";
                c.DataField = "DateField";

                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTime);
        }

        public void ModelBindingForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var c = CreateControl();
                c.SelectMethod = "GetNull";
                c.DataField = "DateField";

                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTimeNull);
        }

        public void DataSourceIdForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var c = CreateControl();
                c.DataSourceID = "ModelDataSource";
                c.DataField = "DateField";
                c.ItemType = "ExitStrategy.ForWebforms.Tests.Mocks.MockModel";

                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTime);
        }

        public void DataSourceForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var c = CreateControl();
                c.DataSource = MockModel.Default();
                c.DataField = "DateField";

                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTime);
        }

        public void FormViewForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var f = new FormView
                {
                    DataSourceID = "ModelDataSource",
                    ItemType = "ExitStrategy.ForWebforms.Tests.Mocks.MockModel"
                };
                p.Controls.Add(f);

                var c = CreateControl();
                c.DataField = "DateField";

                f.ItemCreated += (sender, args) =>
                {
                    (sender as FormView).Controls.Add(c);
                };

                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTime);
        }
    }
}