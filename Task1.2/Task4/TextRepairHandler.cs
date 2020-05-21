using System.Linq;
using System.Text;
using Common;

namespace Task4
{
    internal class TextRepairHandler : StringHandler
    {
        private char[] SentencesSeparators { get; } = new char[] { '.', '!', '?' };

        protected override string HandleData(string data) => Repair(data);

        private string Repair(string text)
        {
            var splitedString = from s in text.Split()
                                where !string.IsNullOrEmpty(s)
                                select s;

            bool isStartWord = true;
            StringBuilder result = new StringBuilder();
            foreach (var s in splitedString)
            {
                StringBuilder temp = new StringBuilder(s);
                if (isStartWord && char.IsLetter(temp[0]))
                    temp[0] = char.ToUpper(temp[0]);

                isStartWord = SentencesSeparators.Contains(s.Last());

                result.Append(temp);
                result.Append(' ');
            }

            return result.ToString().TrimEnd(' ');
        }
    }
}
