using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IModelProvider
    {
        ModelDefinition ExtractModel(IEnumerable dataSource);
    }

    public class ModelProvider : IModelProvider
    {
        private readonly MvcControl _control;

        public ModelProvider(MvcControl control)
        {
            _control = control;
        }

        public ModelDefinition ExtractModel(IEnumerable dataSource)
        {
            return _control.IsModelBound ?
                ExtractModelFromModelBinding(dataSource) :
                ExtractModelFromDataSource(dataSource ?? _control.DataSource);
        }

        private ModelDefinition ExtractModelFromDataSource(object dataSource)
        {
            if(dataSource == null)
                throw new ArgumentNullException("dataSource");

            if (dataSource is IEnumerable)
            {
                return ExtractModelFromDataSource(dataSource as IEnumerable);
            }
            else
            {
                return new ModelDefinition
                {
                    ModelType = GetExpectedType() ?? dataSource.GetType(),
                    Value = dataSource
                };
            }
        }

        private ModelDefinition ExtractModelFromDataSource(IEnumerable dataSource)
        {
            var expectedType = GetExpectedType();

            var value = expectedType == null ? dataSource : ConvertData(dataSource, expectedType);
            var type = value == null ? expectedType : value.GetType();

            return new ModelDefinition
            {
                Value = value,
                ModelType = type
            };
        }

        private ModelDefinition ExtractModelFromModelBinding(IEnumerable source)
        {
            //If the control is databound using the 4.5 SelectMethod, we can determine the 
            //type of the model by looking at the returntype of the SelectMethod
            var returnType = _control.Page.GetType().GetMethod(_control.SelectMethod).ReturnType;

            return new ModelDefinition
            {
                ModelType = returnType,
                Value = ConvertData(source, returnType)
            };
        }

        private Type GetExpectedType()
        {
            //If it's not ModelBinding, the ItemType property can give a hint about the type of the Model
            //Otherwise we just use the type of the object (which will probably be Object[])
            if (!string.IsNullOrEmpty(_control.ItemType))
            {
                return Type.GetType(_control.ItemType);
            }

            return null;
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