using System;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public class ModelDefinition
    {
        public object Value { get; set; }
        public Type ModelType { get; set; }

        public ModelDefinition()
        {

        }

        public ModelDefinition(object value)
        {
            Value = value;
            ModelType = value != null ? value.GetType() : null;
        }
    }
}