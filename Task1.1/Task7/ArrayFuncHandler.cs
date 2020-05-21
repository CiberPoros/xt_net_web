using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Task7
{
    internal class ArrayFuncHandler : Handler
    {
        private readonly static Random _rnd;
        const int DefaultLength = 10;
        const int ValueUpLimit = 100;

        static ArrayFuncHandler()
        {
            _rnd = new Random();
        }

        protected override string StartInfo => 
            $"Enter a positive integer length or press enter for use default length = { DefaultLength }...";

        protected override string HandleData(string data)
        {
            int length = DefaultLength;
            if (data != string.Empty)
                if (!int.TryParse(data, out length))
                    throw new ArgumentException($"parse \"{ data }\" failed", nameof(data));

            try
            {
                StringBuilder result = new StringBuilder();

                result.Append($"Inicializing collection:{ Environment.NewLine }");
                List<int> collection = CollectionsGenerator.GenerateCollection(length, () => _rnd.Next(ValueUpLimit)).ToList();
                collection.ForEach(element => result.Append($"{ element } "));
                result.Append(Environment.NewLine);

                var max = HeaviestElementUtil.GetHeaviestElement(collection, (a, b) => a.CompareTo(b));
                result.Append($"Max element searching:{ Environment.NewLine }max = { max }{ Environment.NewLine }");

                var min = HeaviestElementUtil.GetHeaviestElement(collection, (a, b) => b.CompareTo(a));
                result.Append($"Mix element searching:{ Environment.NewLine }min = { min }{ Environment.NewLine }");

                result.Append($"Sorting collection:{ Environment.NewLine }");
                SortUtil.Sort(collection);
                collection.ForEach(element => result.Append($"{ element } "));
                result.Append(Environment.NewLine);

                return result.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}
