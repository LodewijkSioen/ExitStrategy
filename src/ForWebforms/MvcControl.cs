using ExitStrategy.ForWebforms.ModelBinding;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace ExitStrategy.ForWebforms
{
    public abstract class MvcControl : DataBoundControl, IBindableControl, IDataSourceViewSchemaAccessor
    {
        private ModelDefinition _modelDefinition;
        private readonly IModelProvider _modelProvider;
        private readonly IModelValueExtractor _modelExtractor;

        protected MvcControl(IModelProvider provider = null, IModelValueExtractor extractor = null)
        {
            _modelProvider = provider ?? new ModelProvider(this);
            _modelExtractor = extractor ?? new ModelValueExtractor(this);
        }

        public override bool EnableViewState 
        {
            get { return false; }
            set { throw new NotImplementedException("Friends dont let friends use the viewstate."); }
        }

        public bool IsModelBound
        {
            get
            {
                return base.IsUsingModelBinders;
            }
        }

        [TypeConverter(typeof(DataSourceViewSchemaConverter))]
        public string DataField { get; set; }

        protected override void ValidateDataSource(object dataSource)
        {
            //Do nothing, we accept anything
        }

        protected override void PerformDataBinding(IEnumerable data)
        {
            _modelDefinition = _modelProvider.ExtractModel(data);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var viewBag = new ViewDataDictionary();
            viewBag.ModelState.AdaptModelState(Page.ModelState);

            if (_modelDefinition != null)
            {
                if (string.IsNullOrEmpty(DataField))
                {
                    viewBag.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null,
                        _modelDefinition.ModelType);
                    viewBag.Model = _modelDefinition.Value;
                    viewBag.TemplateInfo.HtmlFieldPrefix = ClientID;
                }
                else
                {
                    var dataItem = DataBinder.GetDataItem(DataItemContainer);
                    viewBag.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForProperty(() => null,
                        dataItem.GetType(), DataField);
                    viewBag.Model = DataBinder.GetPropertyValue(dataItem, DataField);
                    viewBag.TemplateInfo.HtmlFieldPrefix = ClientID + "." + DataField;
                }
            }
            
            var helper = MvcBridge.CreateHtmlHelper(HttpContextProvider.Current.Request.RequestContext, viewBag, writer);

            var markup = RenderMvcContent(helper, viewBag);

            writer.Write(markup.ToString());
        }

        protected abstract MvcHtmlString RenderMvcContent(HtmlHelper helper, ViewDataDictionary viewBag);
        
        public void ExtractValues(IOrderedDictionary dictionary)
        {
            foreach (var value in _modelExtractor.ExtractValues(HttpContextProvider.Current.Request.Form))
            {
                dictionary.Add(value.Key, value.Value);
            }
        }

        public System.Web.ModelBinding.IValueProvider GetValueProvider()
        {
            var nameValueCollection = new NameValueCollection();
            foreach (var value in _modelExtractor.ExtractValues(HttpContextProvider.Current.Request.Form))
            {
                nameValueCollection.Add(value.Key, value.Value);
            }
            return new System.Web.ModelBinding.NameValueCollectionValueProvider(nameValueCollection, CultureInfo.CurrentCulture);
        }

        object IDataSourceViewSchemaAccessor.DataSourceViewSchema
        {
            get; set;
        }
    }
}