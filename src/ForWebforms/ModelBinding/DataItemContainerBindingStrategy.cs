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
            var dataItem = DataBinder.GetDataItem(_control.DataItemContainer);
            if (dataItem != null)
            {
                var metaData = ModelMetadataProviders.Current.GetMetadataForType(() => null, dataItem.GetType());
                return new ModelDefinition(metaData, dataItem);
            }

            var dataBoundControl = FindDataboundControl(_control);
            
            var typeName = dataBoundControl.ItemType;
            if (string.IsNullOrEmpty(typeName))
            {
                throw new InvalidOperationException(string.Format("Cannot determine the databinding type for control with id '{0}'. Please provide the correct type in the ItemType property of the control.", dataBoundControl.ID));
            }

            var type = BuildManager.GetType(typeName, true, false);
            var metaDataForItemType = ModelMetadataProviders.Current.GetMetadataForType(() => null, type);

            return new ModelDefinition(metaDataForItemType, null);
        }

        public DataBoundControl FindDataboundControl(Control control)
        {
            var parent = control.DataKeysContainer;
            var parentAsDataBoundControl = parent as DataBoundControl;
            if (parentAsDataBoundControl == null)
            {
                throw new InvalidOperationException(string.Format("Cannot determine the DataBoundControl the control with ID '{0}' is nested in. Make sure the control is nested within the template of a DataBoundControl (eg. ListView, FormView,..)", _control.ID));
            }
            return parentAsDataBoundControl;
        }
    }
}