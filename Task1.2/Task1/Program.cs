﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler.CreateInstance<AverageLengthHandler>()
                   .StartHandle();
        }
    }
}
