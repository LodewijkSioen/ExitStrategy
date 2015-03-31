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

            if (control.DataSource != null) return new DataSourceBindingStrategy(control);

            if (!string.IsNullOrEmpty(control.DataSourceID)) return new DataSourceBindingStrategy(control);

            return new DataItemContainerBindingStrategy(control);
        }
    }
}