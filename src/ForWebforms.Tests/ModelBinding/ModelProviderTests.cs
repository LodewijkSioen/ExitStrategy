using ExitStrategy.ForWebforms;
using ExitStrategy.ForWebforms.ModelBinding;
using ForWebforms.Tests.Mocks;
using Moq;
using Shouldly;
using System;

namespace ForWebforms.Tests.ModelBinding
{
    public class ModelProviderTests
    {
        private WebformsScaffold Host;

        public ModelProviderTests()
        {
            Host = WebformsScaffold.Create();
        }

        public void TestModelBindingWithData()
        {
            Host.Test(
                (p, w) => 
                {
                    var expectedModel = new DateTime(2014, 12, 28);
                    var control = new MockControl();
                    control.SelectMethod = "GetModel";
                    p.SetControlUnderTest(control);
                    var provider = new ModelProvider(control);

                    var result = provider.ExtractModel(new []{expectedModel});
                    result.Value.ShouldBe((object)new DateTime(2014, 12, 28));
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }

        public void TestModelBindingWithEmptyData()
        {
            Host.Test(
                (p, w) =>
                {
                    var control = new MockControl();
                    control.SelectMethod = "GetModel";
                    p.SetControlUnderTest(control);
                    var provider = new ModelProvider(control);

                    var result = provider.ExtractModel(new Object[0]);
                    result.Value.ShouldBe(null);
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }

        public void TestModelBindingWithNull()
        {
            Host.Test(
                (p, w) =>
                {
                    var control = new MockControl();
                    control.SelectMethod = "GetModel";
                    p.SetControlUnderTest(control);
                    var provider = new ModelProvider(control);

                    var result = provider.ExtractModel(null);
                    result.Value.ShouldBe(null);
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }
    }
}
