﻿using Common;

namespace Task2
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<CharsRepeatHandler>()
                   .StartHandle();
        }
    }
}
