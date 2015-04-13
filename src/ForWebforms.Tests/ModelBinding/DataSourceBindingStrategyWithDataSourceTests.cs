using System;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public class DataSourceBindingStrategyWithDataSourceTests : BaseBindingStrategyTests
    {
        private DataSourceBindingStrategy ArrangeWithDataSource(MockPage p, object value, string typeName)
        {
            var control = new MockControl { DataSource = value, ItemType = typeName };
            p.Controls.Add(control);
            return new DataSourceBindingStrategy(control);
        }

        public void TestWithDataSourceAndItemType()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, _expectedModel, _modelName);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                });
        }

        public void TestWithDataSourceAndNoItemType()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, _expectedModel, null);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                });
        }

        public void TestWithDataSourceNullButItemTypeNot()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, null, _modelName);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(null);
                    result.MetaData.ModelType.ShouldBe(_modelType);
                });
        }

        public void TestWithDataSourceNullAndNoItemType()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, null, null);

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(null);
                    result.MetaData.ShouldBe(null);
                });
        }
    }
}