﻿using Common;

namespace Task3
{
    class Program
    {
        static void Main()
        {
            Handler.CreateInstance<LowerCasesHandler>()
                   .StartHandle();
        }
    }
}
