using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Task1
{
    internal class AverageLengthHandler : StringHandler
    {
        protected override string ResultInfo => "Average length (not an integer):";

        protected override string HandleData(string data) => Parse(data).Average(s => s.Length)
                                                                        .ToString("#.###");
    }
}
