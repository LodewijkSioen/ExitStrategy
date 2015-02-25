using System.Web.Mvc;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public class ModelDefinition
    {
        public object Model { get; set; }
        public ModelMetadata MetaData { get; set; }

        public ModelDefinition(ModelMetadata metadata, object model)
        {
            Model = model;
            MetaData = metadata;
        }
    }
}