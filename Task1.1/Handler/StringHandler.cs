using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class StringHandler : Handler
    {
        protected override string StartInfo => "Enter some string...";

        protected char[] Separators { get; set; } = new char[] { '\n', '\r', ' ', ',', '.', '?', '!', ':', ';', '\'', '"', '(', ')', '-', '{', '}', '[', ']', '\\', '/' };

        protected ICollection<string> Parse(string @string) =>
            (from subStr in @string.Split(Separators)
             where !string.IsNullOrEmpty(subStr)
             select subStr)
            .ToList();
    }
}
