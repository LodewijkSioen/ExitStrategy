namespace ExitStrategy.ForWebforms.Tests
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