namespace SuperString
{
    public static class StringExtensions
    {
        private const char firstEndSymbol = 'a';
        private const char lastEndSymbol = 'z';
        private const char firstRusSymbol = 'а';
        private const char lastRusSymbol = 'я';
        private const char firstNumSymbol = '0';
        private const char lastNumSymbol = '9';
        private const char isolatedRusSymbol = 'ё';

        private static readonly char[] _defaultSeparators = new char[] 
        {
            ' ',
            ',',
            '.',
            '?',
            ';',
            ':',
            '\'',
            '"',
            '|',
            '\\',
            '/',
            '!',
            '@',
            '-',
            '_',
            '\r',
            '\n',
        };

        public static LanguageType GetLanguageType(this string str, char[] separators)
        {
            string[] words = str.ToLower().Split(separators);

            LanguageType result = LanguageType.None;
            foreach (var word in words)
            {
                foreach (var c in word)
                {
                    if (InRange(firstEndSymbol, lastEndSymbol, c))
                        result |= LanguageType.English;
                    else if (InRange(firstRusSymbol, lastRusSymbol, c) || c == isolatedRusSymbol)
                        result |= LanguageType.Russian;
                    else if (InRange(firstNumSymbol, lastNumSymbol, c))
                        result |= LanguageType.Number;
                    else
                        result |= LanguageType.Unknown;
                }
            }

            return result;
        }

        public static LanguageType GetLanguageType(this string str) => GetLanguageType(str, _defaultSeparators);

        private static bool InRange(char first, char last, char currentSymbol) => currentSymbol >= first && currentSymbol <= last;
    }
}
