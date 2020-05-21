using System.Linq;
using Common;

namespace Task3
{
    internal class LowerCasesHandler : StringHandler
    {
        protected override string ResultInfo => "Count of words than started with lowercases:";

        protected override string HandleData(string data) => 
            (from s in Parse(data)
             where char.IsLower(s.First())
             select s)
            .Count()
            .ToString();
    }
}
