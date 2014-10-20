using System;
using System.Linq;
using System.Web.ModelBinding;
using TestWebsite.Models;

namespace TestWebsite
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            View = ViewModel.Default;
        }

        public ViewModel View { get; set; }

        protected void WebFormsClick(object sender, EventArgs e)
        {
            var post = new PostModel();
            TryUpdateModel(post, new FormValueProvider(ModelBindingExecutionContext));
            var x = ModelState.IsValid;
        }

        
    }
}