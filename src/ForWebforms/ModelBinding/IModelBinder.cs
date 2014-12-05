using IMvcModelBinder = System.Web.Mvc.IModelBinder;
using IWebformsModelBinder = System.Web.ModelBinding.IModelBinder;

namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IModelBinder : IMvcModelBinder, IWebformsModelBinder
    {
    }
}
