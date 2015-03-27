namespace ExitStrategy.ForWebforms.Tests.Controls
{
    public abstract class MvcControlTests
    {
        protected readonly WebformsScaffold Host;

        protected MvcControlTests()
        {
            Host = WebformsScaffold.Create();
        }
    }
}