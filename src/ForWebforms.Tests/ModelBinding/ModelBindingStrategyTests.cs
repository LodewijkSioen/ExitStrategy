using System;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public class ModelBindingStrategyTests : BaseBindingStrategyTests
    {
        private ModelBindingStrategy ArrangeWithModelBinding(MockPage p)
        {
            var control = new MockControl() { SelectMethod = "GetModel" };
            p.Controls.Add(control);
            return new ModelBindingStrategy(control);
        }

        public void TestModelBindingWithData()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithModelBinding(p);

                    var result = provider.ExtractModel(new[] { _expectedModel });

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ModelType.ShouldBe(typeof(MockModel));
                }
                );
        }

        public void TestModelBindingWithEmptyData()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithModelBinding(p);

                    var result = provider.ExtractModel(new Object[0]);

                    result.Model.ShouldBe(null);
                    result.MetaData.ModelType.ShouldBe(typeof(MockModel));
                }
                );
        }

        public void TestModelBindingWithNull()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithModelBinding(p);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(null);
                    result.MetaData.ModelType.ShouldBe(typeof(MockModel));
                }
                );
        }
    }
}