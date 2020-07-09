using System;

namespace SuperString
{
    [Flags]
    public enum LanguageType : byte
    {
        None = 0,
        Russian = 1,
        English = 2,
        Number = 4,
        Unknown = 8,
    }
}
