using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public class DataItemContainerBindingStrategyTests : BaseBindingStrategyTests
    {
        private DataItemContainerBindingStrategy ArrangeWithFormView(MockPage p, string typeName, DateTime expectedModel)
        {
            var control = new MockControl();
            var formView = new FormView()
            {
                ItemType = typeName,
                DataSource = new[]{ expectedModel }
            };
            formView.ItemCreated += (sender, args) =>
            {
                (sender as FormView).Controls.Add(control);
            };
            p.Controls.Add(formView);
            p.DataBind();
            return new DataItemContainerBindingStrategy(control);
        }

        public void InsideFormViewWithDataItem()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithFormView(p, null, _expectedModel);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ModelType.ShouldBe(typeof (DateTime));
                });
        }

        public void InsideListViewWithDataItem()
        {
            _host.Test(
                (p, w) =>
                {
                    var control = new MockControl();
                    DataItemContainerBindingStrategy strategy = null;
                    var listView = new ListView()
                    {
                        ItemType = null,
                        DataSource = new[] { _expectedModel },
                        ItemTemplate = new TemplateBuilder()
                    };
                    listView.ItemCreated += (sender, args) =>
                    {
                        args.Item.Controls.Add(control);
                        strategy = new DataItemContainerBindingStrategy(control);
                        var result = strategy.ExtractModel(null);

                        result.Model.ShouldBe(_expectedModel);
                        result.MetaData.ModelType.ShouldBe(typeof(DateTime));
                    };
                    p.Controls.Add(listView);
                    p.DataBind();
                });
        }
    }
}