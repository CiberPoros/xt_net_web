using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Task9
{
    internal class NonNegativeSumHandler : Handler
    {
        private readonly static Random _rnd;
        const int DefaultLength = 10;
        const int ValueDownLimit = -100;
        const int ValueUpLimit = 100;

        static NonNegativeSumHandler()
        {
            _rnd = new Random();
        }

        protected override string StartInfo => "Generating array with 10 elements and calculating summ non negative elements..."; 

        protected override string ReadData() => null;

        protected override bool MakePause => true;

        protected override string HandleData(string data)
        {
            var result = new StringBuilder();

            result.Append($"Generated collection:{ Environment.NewLine }");
            ICollection<int> collection = CollectionsGenerator.
                GenerateCollection(DefaultLength, () => _rnd.Next(ValueDownLimit, ValueUpLimit));

            foreach (var element in collection)
                result.Append($"{ element } ");
            result.Append(Environment.NewLine);

            result.Append($"Calculated summ:{ Environment.NewLine }");
            result.Append(SummByPredicate(collection, element => element > 0, (a, b) => a + b));

            return result.ToString();
        }

        private T SummByPredicate<T>(ICollection<T> collection, Predicate<T> isSummable, Func<T, T, T> summator)
        {
            T res = default;

            foreach (var element in collection)
                if (isSummable(element))
                    res = summator(res, element);

            return res;
        }
    }
}
