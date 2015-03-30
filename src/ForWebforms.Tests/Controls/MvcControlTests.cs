using System;

namespace ExitStrategy.ForWebforms.Tests.Controls
{
    [Serializable]
    public abstract class MvcControlTests
    {
        protected readonly WebformsScaffold Host;

        protected MvcControlTests()
        {
            Host = WebformsScaffold.Create();
        }
    }
}