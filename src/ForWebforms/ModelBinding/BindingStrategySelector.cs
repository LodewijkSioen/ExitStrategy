namespace ExitStrategy.ForWebforms.ModelBinding
{
    public interface IBindingStrategySelector
    {
        IBindingStrategy GetStrategy(MvcControl control);
    }

    public class BindingStrategySelector : IBindingStrategySelector
    {
        public IBindingStrategy GetStrategy(MvcControl control)
        {
            if (control.IsModelBound) return new ModelBindingStrategy(control);

            if(control.DataItemContainer != null) return new DataItemContainerBindingStrategy(control);

            return new DataSourceBindingStrategy(control);
        }
    }
}