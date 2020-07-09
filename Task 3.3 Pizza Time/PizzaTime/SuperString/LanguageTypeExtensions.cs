namespace SuperString
{
    public static class LanguageTypeExtensions
    {
        public static bool IsMixed(this LanguageType languageType)
        {
            if (languageType.HasFlag(LanguageType.Unknown))
                return true;

            int cntSingleBits = 0;
            while (languageType != LanguageType.None)
            {
                cntSingleBits++;

                if (cntSingleBits > 1)
                    return true;

                languageType &= languageType - 1;
            }

            return false;
        }
    }
}
