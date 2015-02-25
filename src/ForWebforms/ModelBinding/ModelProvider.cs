using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IModelProvider
    {
        ModelDefinition ExtractModel(IEnumerable dataSource);
    }

    public enum BindingStrategy
    {
        Model,
        DataSource,
        DataItemContainer
    }

    public class ModelProvider : IModelProvider
    {
        private readonly MvcControl _control;

        public ModelProvider(MvcControl control)
        {
            _control = control;
        }

        public BindingStrategy GetBindingStrategy()
        {
            if(_control.IsModelBound) return BindingStrategy.Model;
            if(!string.IsNullOrEmpty(_control.DataField)) return BindingStrategy.DataItemContainer;
            return BindingStrategy.DataSource;
        }

        public ModelDefinition ExtractModel(IEnumerable dataSource)
        {
            var strategy = GetBindingStrategy();
            switch (strategy)
            {
                case BindingStrategy.Model:
                    return ExtractModelFromModelBinding(dataSource);
                case BindingStrategy.DataSource:
                    return ExtractModelFromDataSource(dataSource ?? _control.DataSource);
                case BindingStrategy.DataItemContainer:
                    return ExtractModelFromDataItemContainer();
                default:
                    throw new NotImplementedException(string.Format("BindingStrategy not implemented: {0}", strategy));
            }
        }

        private ModelDefinition ExtractModelFromDataItemContainer()
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

        private ModelDefinition ExtractModelFromModelBinding(IEnumerable source)
        {
            //If the control is databound using the 4.5 SelectMethod, we can determine the 
            //type of the model by looking at the returntype of the SelectMethod
            var returnType = _control.Page.GetType().GetMethod(_control.SelectMethod).ReturnType;
            var modelMetaData = ModelMetadataProviders.Current.GetMetadataForType(() => null, returnType);

            return new ModelDefinition(modelMetaData, ConvertData(source, returnType));
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

        private object ConvertData(IEnumerable source, Type expectedType)
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
    }
}