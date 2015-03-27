using System.Web.Mvc;

namespace ExitStrategy.ForWebforms.Bridge
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