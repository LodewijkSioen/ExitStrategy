using System;
using System.Collections;

namespace ExitStrategy.ForWebforms.ModelBinding
{
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
}