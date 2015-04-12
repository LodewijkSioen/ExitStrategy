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
            var dataContainer = _control.DataItemContainer;
            var dataItem = DataBinder.GetDataItem(dataContainer);

            if (dataItem != null)
            {
                var metaData = ModelMetadataProviders.Current.GetMetadataForType(() => null, dataItem.GetType());
                return new ModelDefinition(metaData, dataItem);
            }

            var dataBoundControl = FindDataboundControl(dataContainer);
            if (dataBoundControl == null)
            {
                throw new InvalidOperationException(string.Format("Cannot determine the DataBoundControl that control with id '{0}' is nested in. Are you sure this control is used in a template for a DataBoundControl?", dataContainer.ID));
            }

            var typeName = dataBoundControl.ItemType;
            if (string.IsNullOrEmpty(typeName))
            {
                throw new InvalidOperationException(string.Format("Cannot determine the databinding type for control with id '{0}'. Please provide the correct type in the ItemType property of the control.", dataContainer.ID));
            }

            var type = BuildManager.GetType(typeName, true, false);
            var metaDataForItemType = ModelMetadataProviders.Current.GetMetadataForType(() => null, type);

            return new ModelDefinition(metaDataForItemType, null);
        }

        private DataBoundControl FindDataboundControl(Control control)
        {
            while (control != null)
            {
                var databoundControl = control as DataBoundControl;
                if (databoundControl != null)
                    return databoundControl;

                control = control.NamingContainer;
            }
            return null;
        }
    }
}