using System;

namespace Task6
{
    [Flags]
    internal enum Font
    {
        None = 0,
        Bold = 0b1,
        Italic = 0b10,
        Underline = 0b100,
    }
}
