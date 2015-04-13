using System;
using ExitStrategy.ForWebforms.Tests.Controls;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public abstract class BaseBindingStrategyTests
    {
        protected readonly WebformsScaffold _host = WebformsScaffold.Default;
        protected readonly BindingStrategyTestModel _expectedModel = new BindingStrategyTestModel();
        protected readonly Type _modelType = typeof(BindingStrategyTestModel);
        protected readonly string _modelName = typeof(BindingStrategyTestModel).FullName;

        [Serializable]
        public class BindingStrategyTestModel { }
    }
}