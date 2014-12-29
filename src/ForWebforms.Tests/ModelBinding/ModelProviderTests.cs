using System;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    public class ModelProviderTests
    {
        private readonly WebformsScaffold _host;

        public ModelProviderTests()
        {
            _host = WebformsScaffold.Create();
        }

        public void TestModelBindingWithData()
        {
            _host.Test(
                (p, w) => 
                {
                    var expectedModel = new DateTime(2014, 12, 28);
                    var control = new MockControl();
                    control.SelectMethod = "GetModel";
                    p.Controls.Add(control);
                    var provider = new ModelProvider(control);

                    var result = provider.ExtractModel(new []{expectedModel});
                    result.Value.ShouldBe((object)new DateTime(2014, 12, 28));
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }

        public void TestModelBindingWithEmptyData()
        {
            _host.Test(
                (p, w) =>
                {
                    var control = new MockControl();
                    control.SelectMethod = "GetModel";
                    p.Controls.Add(control);
                    var provider = new ModelProvider(control);

                    var result = provider.ExtractModel(new Object[0]);
                    result.Value.ShouldBe(null);
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }

        public void TestModelBindingWithNull()
        {
            _host.Test(
                (p, w) =>
                {
                    var control = new MockControl();
                    control.SelectMethod = "GetModel";
                    p.Controls.Add(control);
                    var provider = new ModelProvider(control);

                    var result = provider.ExtractModel(null);
                    result.Value.ShouldBe(null);
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }
    }
}
