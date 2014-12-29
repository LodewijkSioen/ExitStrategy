using System;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public class ModelProviderTests
    {
        private readonly WebformsScaffold _host;

        public ModelProviderTests()
        {
            _host = WebformsScaffold.Create();
        }

        private ModelProvider ArrangeWithModelBinding(MockPage p)
        {
            var control = new MockControl { SelectMethod = "GetModel" };
            p.Controls.Add(control);
            return new ModelProvider(control);
        }

        public void TestModelBindingWithData()
        {
            _host.Test(
                (p, w) => 
                {
                    var expectedModel = new DateTime(2014, 12, 28);
                    var provider = ArrangeWithModelBinding(p);

                    var result = provider.ExtractModel(new []{expectedModel});

                    result.Value.ShouldBe(expectedModel);
                    result.ModelType.ShouldBe(typeof(DateTime));
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
                    var provider = ArrangeWithModelBinding(p);

                    var result = provider.ExtractModel(null);

                    result.Value.ShouldBe(null);
                    result.ModelType.ShouldBe(typeof(DateTime));
                }
            );
        }

        private ModelProvider ArrangeWithDataSource(MockPage p, object value, string typeName)
        {
            var control = new MockControl { DataSource = value, ItemType = typeName};
            p.Controls.Add(control);
            control.DataBind();
            return new ModelProvider(control);
        }

        public void TestWithDataSource()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, new DateTime(2014, 12, 18), "System.DateTime");

                    var result = provider.ExtractModel(null);

                    result.Value.ShouldBe(new DateTime(2014, 12, 18));
                    result.ModelType.ShouldBe(typeof(DateTime));
                });
        }
    }
}
