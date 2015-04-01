using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;
using Moq;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    public class BindingStrategySelectorTests
    {
        readonly BindingStrategySelector _selector = new BindingStrategySelector();

        private void GetStrategyModelBound()
        {
            var control = new Mock<MvcControl>(null, null);
            control.Setup(c => c.IsModelBound).Returns(true);

            var strategy = _selector.GetStrategy(control.Object);

            strategy.ShouldBeOfType<ModelBindingStrategy>();
        }

        private void GetStrategyDataItemContainer()
        {
            var control = new Mock<MvcControl>(null, null);
            control.Setup(c => c.DataItemContainer).Returns(new FormView());

            var strategy = _selector.GetStrategy(control.Object);

            strategy.ShouldBeOfType<DataItemContainerBindingStrategy>();
        }

        private void GetStrategyDataSource()
        {
            var control = new Mock<MvcControl>(null, null);

            var strategy = _selector.GetStrategy(control.Object);

            strategy.ShouldBeOfType<DataSourceBindingStrategy>();
        }
    }
}
