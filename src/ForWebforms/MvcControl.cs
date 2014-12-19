using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExitStrategy.ForWebforms.ModelBinding;

namespace ExitStrategy.ForWebforms
{
    public abstract class MvcControl : DataBoundControl, IBindableControl
    {
        private ModelDefinition _modelDefinition;
        private readonly IModelProvider _modelProvider;
        private readonly IModelValueExtractor _modelExtractor;

        protected MvcControl(IModelProvider provider = null, IModelValueExtractor extractor = null)
        {
            _modelProvider = provider ?? new ModelProvider(this);
            _modelExtractor = extractor ?? new ModelValueExtractor(this);
        }

        public override bool EnableViewState { get{return false;} }

        protected override void ValidateDataSource(object dataSource)
        {
            //Do nothing, we accept anything
        }

        protected override void PerformDataBinding(IEnumerable data)
        {
            _modelDefinition = IsUsingModelBinders ? 
                _modelProvider.ExtractModelFromModelBinding(data) : 
                _modelProvider.ExtractModelFromDataSource(data ?? DataSource);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var viewBag = new ViewDataDictionary();
            viewBag.ModelState.AdaptModelState(Page.ModelState);

            if (_modelDefinition != null)
            {
                viewBag.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, _modelDefinition.ModelType);
                viewBag.Model = _modelDefinition.Value;
                viewBag.TemplateInfo.HtmlFieldPrefix = ClientID;
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
    }
}