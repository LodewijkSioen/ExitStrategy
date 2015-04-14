using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    public class BindingStrategySelectorTests
    {
        readonly BindingStrategySelector _selector = new BindingStrategySelector();

        public static IEnumerable<Object[]> TestInput()
        {
            yield return new object[]{new Partial()};
            yield return new object[]{new Editor()};
            yield return new object[]{new Display()};
        }

        [InputSource("TestInput")]
        public void GetStrategyModelBound(MvcControl control)
        {
            control.SelectMethod = "GetModel";
            var strategy = _selector.GetStrategy(control);
            strategy.ShouldBeOfType<ModelBindingStrategy>();
        }

        [InputSource("TestInput")]
        public void GetStrategyDataItemContainer(MvcControl control)
        {
            var formView = new FormView();
            formView.Controls.Add(control);

            var strategy = _selector.GetStrategy(control);

            strategy.ShouldBeOfType<DataItemContainerBindingStrategy>();
        }

        [InputSource("TestInput")]
        public void GetStrategyDataSource(MvcControl control)
        {
            var strategy = _selector.GetStrategy(control);

            strategy.ShouldBeOfType<DataSourceBindingStrategy>();
        }
    }
}
