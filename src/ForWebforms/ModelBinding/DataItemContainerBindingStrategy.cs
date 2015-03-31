using System;
using System.Collections;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public class DataItemContainerBindingStrategy : IBindingStrategy
    {
        readonly MvcControl _control;

        public DataItemContainerBindingStrategy(MvcControl control)
        {
            _control = control;
        }

        public ModelDefinition ExtractModel(IEnumerable dataSource)
        {
            bool found;
            var dataContainer = _control.DataItemContainer;
            var dataItem = DataBinder.GetDataItem(dataContainer, out found);

            if (dataItem != null)
            {
                var metaData = ModelMetadataProviders.Current.GetMetadataForType(() => null, dataItem.GetType());
                return new ModelDefinition(metaData, dataItem);
            }

            var typeName = (dataContainer as DataBoundControl).ItemType;
            if (string.IsNullOrEmpty(typeName))
            {
                throw new InvalidOperationException(string.Format("Cannot determine the databinding type for control with id '{0}'. Please provide the correct type in the ItemType property of the control.", dataContainer.ID));
            }

            var type = BuildManager.GetType(typeName, true, false);
            var metaDataForItemType = ModelMetadataProviders.Current.GetMetadataForType(() => null, type);

            return new ModelDefinition(metaDataForItemType, null);
        }

        
    }
}