﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<CharsRepeatHandler>()
                   .StartHandle();
        }
    }
}