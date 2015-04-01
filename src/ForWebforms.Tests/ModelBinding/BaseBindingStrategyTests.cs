using System;
using ExitStrategy.ForWebforms.Tests.Controls;

namespace ExitStrategy.ForWebforms.Tests.ModelBinding
{
    [Serializable]
    public abstract class BaseBindingStrategyTests
    {
        protected readonly WebformsScaffold _host = WebformsScaffold.Default;
        protected readonly DateTime _expectedModel = new DateTime(2014, 12, 28);
    }
}