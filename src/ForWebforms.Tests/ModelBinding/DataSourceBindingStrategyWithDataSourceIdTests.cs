using System;
using System.Linq;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public class DataSourceBindingStrategyWithDataSourceIdTests : BaseBindingStrategyTests
    {
        private DataSourceBindingStrategy ArrangeWithDataSourceId(MockPage p, string typeName)
        {
            var control = new MockControl { DataSourceID = "DataSourceControl", ItemType = typeName };
            var dataSource = new ObjectDataSource {ID = "DataSourceControl"};
            p.Controls.Add(control);
            p.Controls.Add(dataSource);
            return new DataSourceBindingStrategy(control);
        }

        public void TestModelBindingWithDataSourceIdAndTypeName()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, _modelName);

                    var result = provider.ExtractModel(new []{_expectedModel});

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                }
            );
        }

        public void TestModelBindingWithDataSourceIdAndTypeNameButNoValue()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, _modelName);

                    var result = provider.ExtractModel(Enumerable.Empty<BindingStrategyTestModel>());

                    result.Model.ShouldBe(null);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                }
            );
        }

        public void TestModelBindingWithDataSourceIdButNoValueAndTypeName()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, null);

                    var result = provider.ExtractModel(Enumerable.Empty<BindingStrategyTestModel>());

                    result.Model.ShouldBe(new BindingStrategyTestModel[0]);
                    result.MetaData.ModelType.ShouldBe(typeof(BindingStrategyTestModel[]));
                }
            );
        }

        public void TestModelBindingWithDataSourceIdButWithNoTypeName()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, null);

                    var result = provider.ExtractModel(new[] { _expectedModel });

                    result.Model.ShouldBe(new []{ _expectedModel});
                    result.MetaData.ModelType.ShouldBe(typeof(BindingStrategyTestModel[]));
                }
            );
        }

        public void TestModelBindingWithDataSourceIdButWithNoTypeNameAndNullAsValue()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, null);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(null);
                    result.MetaData.ShouldBe(null);
                }
            );
        }
    }
}
