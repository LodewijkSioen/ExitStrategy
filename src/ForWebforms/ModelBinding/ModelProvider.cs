using System;
using System.Collections;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IBindingStrategySelector
    {
        IBindingStrategy GetStrategy(MvcControl control);
    }

    public class BindingStrategySelector : IBindingStrategySelector
    {
        public IBindingStrategy GetStrategy(MvcControl control)
        {
            if (control.IsModelBound) return new ModelBindingStrategy(control);
            if (!string.IsNullOrEmpty(control.DataField)) return new DataItemContainerBindingStrategy(control);
            return new DataSourceBindingStrategy(control);
        }
    }

    public interface IBindingStrategy
    {
        ModelDefinition ExtractModel(IEnumerable dataSource);
    }

    public abstract class EnumerableBasedBindingStrategy : IBindingStrategy
    {
        protected object ConvertData(IEnumerable source, Type expectedType)
        {
            //Webforms Databinding only works with IEnumerables (hello .net 1.0). It has no concept of
            //databinding to a single item. So we need to handle this here: If the type of the model 
            //is not an IEnumerable, we just need the first item in the collection.
            if (source == null)
            {
                return null;
            }
            else if (typeof(IEnumerable).IsAssignableFrom(expectedType))
            {
                return source;
            }
            else
            {
                var enumerator = source.GetEnumerator();
                return enumerator.MoveNext() ? enumerator.Current : null;
            }
        }

        public abstract ModelDefinition ExtractModel(IEnumerable dataSource);
    }

    public class ModelBindingStrategy : EnumerableBasedBindingStrategy
    {
        MvcControl _control;

        public ModelBindingStrategy(MvcControl control)
        {
            _control = control;
        }

        public override ModelDefinition ExtractModel(IEnumerable dataSource)
        {
            //If the control is databound using the 4.5 SelectMethod, we can determine the 
            //type of the model by looking at the returntype of the SelectMethod
            var returnType = _control.Page.GetType().GetMethod(_control.SelectMethod).ReturnType;
            var modelMetaData = ModelMetadataProviders.Current.GetMetadataForType(() => null, returnType);
            var model = ConvertData(dataSource, returnType);

            return new ModelDefinition(modelMetaData, model);
        }
    }

    public class DataItemContainerBindingStrategy : IBindingStrategy
    {
        MvcControl _control;

        public DataItemContainerBindingStrategy(MvcControl control)
        {
            _control = control;
        }

        public ModelDefinition ExtractModel(IEnumerable dataSource)
        {
            var dataItem = DataBinder.GetDataItem(_control.DataItemContainer);
            if (dataItem != null)
            {
                var metaData = ModelMetadataProviders.Current.GetMetadataForProperty(() => null, dataItem.GetType(), _control.DataField);
                return new ModelDefinition(metaData, dataItem);
            }

            var dataBoundControl = _control.DataItemContainer as DataBoundControl;
            if (dataBoundControl != null)
            {
                var typeName = dataBoundControl.ItemType;
                var type = BuildManager.GetType(typeName, true, false);
                var metaData = ModelMetadataProviders.Current.GetMetadataForProperty(() => null, type, _control.DataField);

                return new ModelDefinition(metaData, null);
            }
            return null;
        }
    }

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