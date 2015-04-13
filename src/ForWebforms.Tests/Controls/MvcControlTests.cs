using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    [Serializable]
    public abstract class MvcControlTests
    {
        protected WebformsScaffold Host = WebformsScaffold.Default;

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

        public void DataSourceIdForDataFieldShouldDisplayCorrectValue()
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

        public void DataSourceIdForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var c = CreateControl();
                c.DataSourceID = "NullModelDataSource";
                c.DataField = "DateField";
                c.ItemType = "ExitStrategy.ForWebforms.Tests.Mocks.MockModel";

                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTimeNull);
        }

        public void DataSourceForDataFieldShouldDisplayCorrectValue()
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

        public void DataSourceForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var c = CreateControl();
                c.DataSource = null;
                c.DataField = "DateField";
                c.ItemType = "ExitStrategy.ForWebforms.Tests.Mocks.MockModel";

                p.Controls.Add(c);
                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTimeNull);
        }

        public void FormViewForDataFieldShouldDisplayCorrectValue()
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

        public void FormViewForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var f = new FormView
                {
                    DataSourceID = "NullModelDataSource",
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

            r.ShouldBe(ExpectedContentForDateTimeNull);
        }

        public void ListViewForDataFieldShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var f = new ListView
                {
                    ItemTemplate = new TemplateBuilder(),
                    DataSourceID = "ModelDataSource",
                    ItemType = "ExitStrategy.ForWebforms.Tests.Mocks.MockModel"
                };
                p.Controls.Add(f);

                var c = CreateControl();
                c.DataField = "DateField";

                f.ItemCreated += (sender, args) =>
                {
                    args.Item.Controls.Add(c);
                };

                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTime);
        }

        public void ListViewForDataFieldWithNullShouldDisplayCorrectValue()
        {
            var r = Host.Test((p, w) =>
            {
                var f = new ListView
                {
                    InsertItemTemplate = new TemplateBuilder(),
                    InsertItemPosition = InsertItemPosition.LastItem,
                    DataSourceID = "NullModelDataSource",
                    ItemType = "ExitStrategy.ForWebforms.Tests.Mocks.MockModel",
                };
                p.Controls.Add(f);

                var c = CreateControl();
                c.DataField = "DateField";

                f.ItemCreated += (sender, args) =>
                {
                    args.Item.Controls.Add(c);
                };

                p.DataBind();
                c.RenderControl(w);
            });

            r.ShouldBe(ExpectedContentForDateTimeNull);
        }
    }
}