﻿using Common;

namespace Task8
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<PositiveNumbersChangeHandler>()
                   .StartHandle();
        }
    }
}
