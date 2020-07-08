using System;
using System.Collections.Generic;
using CustomCollections;

namespace DynamicArrayTests
{
    internal static class RandomTestsUtils
    {
        public const int DEFAULT_STEPS_COUNT = 1000;
        public const int DEFAULT_COUNT_OF_ELEMENTS = 1000;
        public const int DEFAULT_RANDOM_ELEMENT_MIN_VALUE = -100;
        public const int DEFAULT_RANDOM_ELEMENT_MAX_VALUE = 100;

        public static readonly Random Random = new Random();

        public static void ActionTest(DynamicArray<int> array, List<int> list, Action<int> testableAction)
        {
            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int randomElement = array[Random.Next(array.Count)];
                testableAction(randomElement);

                randomElement = list[Random.Next(list.Count)];
                testableAction(randomElement);

                randomElement = Random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);
                testableAction(randomElement);
            }
        }

        public static void FillCollections(DynamicArray<int> dymanicArray, List<int> list, int elementsCount)
        {
            for (int i = 0; i < elementsCount; i++)
            {
                int value = Random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);
                dymanicArray.Add(value);
                list.Add(value);
            }
        }

        public static void FillCollections(DynamicArray<int> dymanicArray, List<int> list) => FillCollections(dymanicArray, list, DEFAULT_COUNT_OF_ELEMENTS);

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
