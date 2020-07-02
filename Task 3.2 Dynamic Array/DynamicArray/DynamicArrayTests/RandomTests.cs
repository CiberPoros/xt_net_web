using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomCollections;
using System.Collections.Generic;
using static DynamicArrayTests.Utils;
using System.Linq;

namespace DynamicArrayTests
{
    [TestClass]
    public class RandomTests
    {
        private const int DEFAULT_STEPS_COUNT = 1000;
        private const int DEFAULT_COUNT_OF_ELEMENTS = 10;
        private const int DEFAULT_RANDOM_ELEMENT_MIN_VALUE = -100;
        private const int DEFAULT_RANDOM_ELEMENT_MAX_VALUE = 100;

        private static readonly Random _random = new Random();

        [TestMethod]
        public void AddTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int value = _random.Next();
                array.Add(value);
                list.Add(value);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void AddRangeTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int len = _random.Next(10);
                int[] collection = new int[len];

                for (int j = 0; j < len; j++)
                    collection[j] = _random.Next();

                array.AddRange(collection);
                list.AddRange(collection);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void ClearTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            array.Clear();
            list.Clear();

            if (!HaveEqualsElements(array, list))
                Assert.Fail();

            FillCollections(array, list);

            array.Clear();
            list.Clear();

            if (!HaveEqualsElements(array, list))
                Assert.Fail();
        }

        [TestMethod]
        public void ContainsTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.Contains(randomElement) != list.Contains(randomElement))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void ConvertAllTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            var resArray = array.ConvertAll(val => val.ToString());
            var resList = list.ConvertAll(val => val.ToString());

            if (!HaveEqualsElements(resArray, resList))
                Assert.Fail();
        }

        [TestMethod]
        public void CopyToTest1()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            var resArray = new int[array.Count];
            var resList = new int[list.Count];

            array.CopyTo(resArray);
            list.CopyTo(resList);

            if (!HaveEqualsElements(resArray, resList))
                Assert.Fail();
        }
        [TestMethod]
        public void CopyToTest2()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int incrCount = _random.Next(10);
                var resArray = new int[array.Count + incrCount];
                var resList = new int[list.Count + incrCount];

                int arrayIndex = _random.Next(resArray.Length - array.Count + 1);
                array.CopyTo(resArray, arrayIndex);
                list.CopyTo(resList, arrayIndex);

                if (!HaveEqualsElements(resArray, resList))
                    Assert.Fail();
            }  
        }
        [TestMethod]
        public void CopyToTest3()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int incrCount = _random.Next(10);
                var resArray = new int[array.Count + incrCount];
                var resList = new int[list.Count + incrCount];

                int index = _random.Next(array.Count);
                int count = _random.Next(array.Count - index);
                int arrayIndex = _random.Next(resArray.Length - count + 1);
                array.CopyTo(index, resArray, arrayIndex, count);
                list.CopyTo(index, resList, arrayIndex, count);

                if (!HaveEqualsElements(resArray, resList))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void ExistsTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.Exists(a => a <= randomElement) != list.Exists(a => a <= randomElement))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void FindTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.Find(a => a <= randomElement) != list.Find(a => a <= randomElement))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void FindAllTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (!HaveEqualsElements(array.FindAll(a => a <= randomElement), list.FindAll(a => a <= randomElement)))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void FindIndexTest1()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.FindIndex(a => a <= randomElement) != list.FindIndex(a => a <= randomElement))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void FindIndexTest2()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);

                if (array.FindIndex(index, a => a <= randomElement) != list.FindIndex(index, a => a <= randomElement))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void FindIndexTest3()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);
                int count = _random.Next(array.Count - index);

                if (array.FindIndex(index, count, a => a <= randomElement) != list.FindIndex(index, count, a => a <= randomElement))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void FindLastTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.FindLast(a => a <= randomElement) != list.FindLast(a => a <= randomElement))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void FindLastIndexTest1()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.FindLastIndex(a => a <= randomElement) != list.FindLastIndex(a => a <= randomElement))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void FindLastIndexTest2()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);

                if (array.FindLastIndex(index, a => a <= randomElement) != list.FindLastIndex(index, a => a <= randomElement))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void FindLastIndexTest3()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);
                int count = _random.Next(index + 1);

                if (array.FindLastIndex(index, count, a => a <= randomElement) != list.FindLastIndex(index, count, a => a <= randomElement))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void GetRangeTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int index = _random.Next(array.Count);
                int count = _random.Next(array.Count - index);

                if (!HaveEqualsElements(array.GetRange(index, count), list.GetRange(index, count)))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void IndexOfTest1()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.IndexOf(randomElement) != list.IndexOf(randomElement))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void IndexOfTest2()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);

                if (array.IndexOf(randomElement, index) != list.IndexOf(randomElement, index))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void IndexOfTest3()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);
                int count = _random.Next(array.Count - index);

                if (array.IndexOf(randomElement, index, count) != list.IndexOf(randomElement, index, count))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int randomValue = _random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);
                int index = _random.Next(array.Count + 1);

                array.Insert(index, randomValue);
                list.Insert(index, randomValue);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void LastIndexOfTest1()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                if (array.LastIndexOf(randomElement) != list.LastIndexOf(randomElement))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void LastIndexOfTest2()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);

                if (array.LastIndexOf(randomElement, index) != list.LastIndexOf(randomElement, index))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void LastIndexOfTest3()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            ActionTest(array, list, testableAction);

            void testableAction(int randomElement)
            {
                int index = _random.Next(array.Count);
                int count = _random.Next(index + 1);

                if (array.LastIndexOf(randomElement, index, count) != list.LastIndexOf(randomElement, index, count))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void InsertRangeTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int countElementsInRandomCollection = _random.Next(10);
                int[] randomCollection = new int[countElementsInRandomCollection];

                for (int j = 0; j < countElementsInRandomCollection; j++)
                    randomCollection[j] = _random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);

                int index = _random.Next(array.Count + 1);

                array.InsertRange(index, randomCollection);
                list.InsertRange(index, randomCollection);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        private void ActionTest(DynamicArray<int> array, List<int> list, Action<int> testableAction)
        {
            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int randomElement = array[_random.Next(array.Count)];
                testableAction(randomElement);

                randomElement = list[_random.Next(list.Count)];
                testableAction(randomElement);

                randomElement = _random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);
                testableAction(randomElement);
            }
        }

        private void FillCollections(DynamicArray<int> dymanicArray, List<int> list)
        {
            for (int i = 0; i < DEFAULT_COUNT_OF_ELEMENTS; i++)
            {
                int value = _random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);
                dymanicArray.Add(value);
                list.Add(value);
            }
        }
    }
}
