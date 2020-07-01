using System.Collections.Generic;
using CustomCollections;

namespace DynamicArrayTests
{
    internal static class Utils
    {  
        public static bool HaveEqualsElements<T>(IList<T> dymanicArray, IList<T> list)
        {
            if (dymanicArray.Count != list.Count)
                return false;

            for (int i = 0; i < dymanicArray.Count; i++)
                if (!Equals(dymanicArray[i], list[i]))
                    return false;

            return true;
        }

        public static bool HaveEqualsCapacity<T>(DynamicArray<T> dymanicArray, List<T> list) => dymanicArray.Capacity == list.Capacity;
    }
}
