using System.Collections;
using System.Web.Compilation;
using System.Web.Mvc;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public class DataSourceBindingStrategy : EnumerableBasedBindingStrategy
    {
        MvcControl _control;

        public DataSourceBindingStrategy(MvcControl control)
        {
            _control = control;
        }

        public override ModelDefinition ExtractModel(IEnumerable dataSource)
        {
            return ExtractModelFromDataSource(dataSource ?? _control.DataSource);
        }

        private ModelDefinition ExtractModelFromDataSource(object dataSource)
        {
            var source = dataSource as IEnumerable;
            if (source != null)
            {
                return ExtractModelFromDataSource(source);
            }
            else
            {
                return new ModelDefinition(GetExpectedType(dataSource), dataSource);
            }
        }

        private ModelDefinition ExtractModelFromDataSource(IEnumerable dataSource)
        {
            var modelMetaData = GetExpectedType(dataSource);
            var value = modelMetaData == null ? dataSource : ConvertData(dataSource, modelMetaData.ModelType);
            return new ModelDefinition(modelMetaData, value);
        }

        private ModelMetadata GetExpectedType(object value)
        {
            //If it's not ModelBinding, the ItemType property can give a hint about the type of the Model
            //Otherwise we just use the type of the object (which will probably be Object[])
            if (!string.IsNullOrEmpty(_control.ItemType))
            {
                var type = BuildManager.GetType(_control.ItemType, true, false);
                return ModelMetadataProviders.Current.GetMetadataForType(() => null, type);
            }

            return value != null ? ModelMetadataProviders.Current.GetMetadataForType(() => null, value.GetType()) : null;
        }
    }
}