using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler handler = new PositiveNumbersChangeHandler();
            handler.StartHandle();
        }
    }
}
