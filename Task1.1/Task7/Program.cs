﻿using Common;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<ArrayFuncHandler>()
                   .StartHandle();
        }
    }
}