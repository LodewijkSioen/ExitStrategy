using System;
using System.Linq;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;
using ExitStrategy.ForWebforms.Tests.Mocks;
using Shouldly;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public class ModelProviderTests
    {
        private readonly WebformsScaffold _host;
        private readonly DateTime _expectedModel = new DateTime(2014, 12, 28);

        public ModelProviderTests()
        {
            _host = WebformsScaffold.Create();
        }

        private ModelProvider ArrangeWithModelBinding(MockPage p)
        {
            var control = new MockControl() { SelectMethod = "GetModel" };
            p.Controls.Add(control);
            return new ModelProvider(control);
        }

        public void TestModelBindingWithData()
        {
            _host.Test(
                (p, w) => 
                {
                    var provider = ArrangeWithModelBinding(p);

                    var result = provider.ExtractModel(new []{_expectedModel});

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ShouldBe(typeof(DateTime));
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
                    result.MetaData.ShouldBe(typeof(DateTime));
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
                    result.MetaData.ShouldBe(typeof(DateTime));
                }
            );
        }

        private ModelProvider ArrangeWithDataSource(MockPage p, object value, string typeName)
        {
            var control = new MockControl { DataSource = value, ItemType = typeName};
            p.Controls.Add(control);
            return new ModelProvider(control);
        }

        public void TestWithDataSourceAndItemType()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, _expectedModel, "System.DateTime");

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ShouldBe(typeof(DateTime));
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
                    result.MetaData.ShouldBe(typeof(DateTime));
                });
        }

        public void TestWithDataSourceNullButItemTypeNot()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSource(p, null, "System.DateTime");

                    var result = provider.ExtractModel(null);

                    result.Model.ShouldBe(null);
                    result.MetaData.ShouldBe(typeof(DateTime));
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

        private ModelProvider ArrangeWithDataSourceId(MockPage p, string typeName)
        {
            var control = new MockControl { DataSourceID = "DataSourceControl", ItemType = typeName };
            var dataSource = new ObjectDataSource {ID = "DataSourceControl"};
            p.Controls.Add(control);
            p.Controls.Add(dataSource);
            return new ModelProvider(control);
        }

        public void TestModelBindingWithDataSourceIdAndTypeName()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, "System.DateTime");

                    var result = provider.ExtractModel(new []{_expectedModel});

                    result.Model.ShouldBe(_expectedModel);
                    result.MetaData.ShouldBe(typeof(DateTime));
                }
            );
        }

        public void TestModelBindingWithDataSourceIdAndTypeNameButNoValue()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, "System.DateTime");

                    var result = provider.ExtractModel(Enumerable.Empty<DateTime>());

                    result.Model.ShouldBe(null);
                    result.MetaData.ShouldBe(typeof(DateTime));
                }
            );
        }

        public void TestModelBindingWithDataSourceIdButNoValueAndTypeName()
        {
            _host.Test(
                (p, w) =>
                {
                    var provider = ArrangeWithDataSourceId(p, null);

                    var result = provider.ExtractModel(Enumerable.Empty<DateTime>());

                    result.Model.ShouldBe(new DateTime[0]);
                    result.MetaData.ShouldBe(typeof(DateTime[]));
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
                    result.MetaData.ShouldBe(typeof(DateTime[]));
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
