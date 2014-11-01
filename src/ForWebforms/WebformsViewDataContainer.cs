using System.Web.Mvc;

namespace ExitStrategy.ForWebforms
{
    public class WebformsViewDataContainer : IViewDataContainer
    {
        public WebformsViewDataContainer(ViewDataDictionary viewData)
        {
            ViewData = viewData;
        }

        public ViewDataDictionary ViewData { get; set; }
    }
}