using ExitStrategy.ForWebforms;

namespace ForWebforms.Tests
{
    public abstract class MvcControlTests<T>
        where T : MvcControl, new()
    {
        protected readonly WebformsScaffold Host;

        protected MvcControlTests()
        {
            Host = WebformsScaffold.Create();
        }
    }
}