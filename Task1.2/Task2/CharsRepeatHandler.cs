using System.Linq;
using System.Text;
using Common;

namespace Task2
{
    internal class CharsRepeatHandler : StringHandler
    {
        protected override string StartInfo => "Enter two strings separated by enter-click...";

        protected override string ReadData() => $"{ ReadLine() }{ '\n' }{ ReadLine() }";

        protected override string HandleData(string data)
        {
            var strs = data.Split('\n');
            return RepeatChars(strs[0], strs[1]);
        }

        private string RepeatChars(string doublingString, string fromString)
        {
            var doublingChars = (from char c in fromString
                                 where !Separators.Contains(c)
                                 select c)
                                 .Distinct()
                                 .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var c in doublingString)
                stringBuilder.Append(c, doublingChars.Contains(c) ? 2 : 1);

            return stringBuilder.ToString();
        }
    }
}
