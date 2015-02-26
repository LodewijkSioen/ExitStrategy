using System.Collections;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.ModelBinding
{
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
}