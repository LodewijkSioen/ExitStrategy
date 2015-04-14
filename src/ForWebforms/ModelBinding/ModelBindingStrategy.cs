using System.Collections;
using System.Web.Mvc;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public class ModelBindingStrategy : EnumerableBasedBindingStrategy
    {
        readonly MvcControl _control;

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
}