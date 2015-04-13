using System.Threading;

namespace ExitStrategy.TestWebsite.Helpers
{
    public static class StringHelpers
    {
        public static string Capitalize(this string original)
        {
            return Thread.CurrentThread.CurrentUICulture.TextInfo.ToTitleCase(original);
        }
    }
}