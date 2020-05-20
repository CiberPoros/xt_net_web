using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class RectangleAreaSolver
    {
        const string excString = "Ожидалось 2 вещественных числа, разделенных пробелом";

        static void Main(string[] args)
        {
            for (; ; )
            {
                var input = Console.ReadLine()
                                    .Trim()
                                    .Split()
                                    .Where(s => !string.IsNullOrEmpty(s))
                                    .ToList();

                if (input.Count != 2)
                    throw new Exception();

                foreach (var s in input)
                    if (double.TryParse(s, out _))
                        return;

            }
        }
    }
}
