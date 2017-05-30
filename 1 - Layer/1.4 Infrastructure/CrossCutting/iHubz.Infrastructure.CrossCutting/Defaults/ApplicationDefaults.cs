namespace iHubz.Infrastructure.CrossCutting.Defaults
{
    public class ApplicationDefaults
    {
        private const string HAS_UNSAFE_CHARACTERS = "</?.+?>";

        public static string RegExHasUnsafeCharacters
        {
            get { return HAS_UNSAFE_CHARACTERS; }
        }
    }
}
