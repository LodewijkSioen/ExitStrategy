using System.Collections;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IBindingStrategy
    {
        ModelDefinition ExtractModel(IEnumerable dataSource);
    }
}