namespace ExitStrategy.ForWebforms
{
    internal static class Version
    {
        public const string VersionNumber = "0.0.2";
        private const bool IsPreRelease = true;

        public const string SemverVersionNumber = VersionNumber + (IsPreRelease ? "-alfa" : "");
    }
}