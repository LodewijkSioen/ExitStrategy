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
        private DataItemContainerBindingStrategy ArrangeWithFormView(MockPage p, string typeName, BindingStrategyTestModel expectedModel)
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

        private void ArrangeActAssertWithListView(MockPage p, string typeName, BindingStrategyTestModel expectedModel)
        {
            var circuitBreaker = false; //Sanity check that the event is actually called
            var control = new MockControl();
            var listView = new ListView()
            {
                ItemType = typeName,
                DataSource = new[] { expectedModel },
                ItemTemplate = new TemplateBuilder()
            };
            listView.ItemCreated += (sender, args) =>
            {
                args.Item.Controls.Add(control);
            };
            listView.ItemDataBound += (sender, args) =>
            {
                var strategy = new DataItemContainerBindingStrategy(control);
                var result = strategy.ExtractModel(null);

                result.Model.ShouldBe(expectedModel);
                result.MetaData.ModelType.ShouldBe(_modelType);
                circuitBreaker = true;
            };
            
            p.Controls.Add(listView);
            p.DataBind();
            circuitBreaker.ShouldBe(true);
        }

        public void InsideFormViewWithDataItem()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithFormView(p, null, _expectedModel);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                });
        }

        public void InsideFormViewWithoutDataItemAndItemTypeShouldThrow()
        {
            var ex = _host.Throws<InvalidOperationException>(
                (p, w) =>
                {
                    var provider = ArrangeWithFormView(p, null, null);

                    provider.ExtractModel(null);
                });

            ex.Message.ShouldBe("Cannot determine the databinding type for control with id ''. Please provide the correct type in the ItemType property of the control.");
        }

        public void InsideFormViewWithouDataItemAndWithItemType()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithFormView(p, _modelName, null);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(null);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                });
        }

        public void InsideListViewWithDataItem()
        {
            _host.Test(
                (p, w) =>
                {
                    ArrangeActAssertWithListView(p, null, _expectedModel);
                });
        }

        public void InsideListViewWithoutDataItemAndWithoutItemTypeShouldThrow()
        {
            var ex = _host.Throws<InvalidOperationException>(
                (p, w) =>
                {
                    ArrangeActAssertWithListView(p, null, null);
                });
            ex.Message.ShouldBe("Cannot determine the databinding type for control with id 'ctrl0'. Please provide the correct type in the ItemType property of the control.");
        }

        public void InsideListViewWithoutDataItemAndWithItemType()
        {
            _host.Test(
                (p, w) =>
                {
                    ArrangeActAssertWithListView(p, _modelName, null);
                });
        }
    }
}