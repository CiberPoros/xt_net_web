using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomCollections;
using System.Collections.Generic;
using static DynamicArrayTests.RandomTestsUtils;

namespace DynamicArrayTests
{
    [TestClass]
    public class DynamicArrayRandomTests
    {
        [TestMethod]
        public void AddTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int value = RandomTestsUtils.Random.Next();
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
                int len = RandomTestsUtils.Random.Next(10);
                int[] collection = new int[len];

                for (int j = 0; j < len; j++)
                    collection[j] = RandomTestsUtils.Random.Next();

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
                int incrCount = RandomTestsUtils.Random.Next(10);
                var resArray = new int[array.Count + incrCount];
                var resList = new int[list.Count + incrCount];

                int arrayIndex = RandomTestsUtils.Random.Next(resArray.Length - array.Count + 1);
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
                int incrCount = RandomTestsUtils.Random.Next(10);
                var resArray = new int[array.Count + incrCount];
                var resList = new int[list.Count + incrCount];

                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(array.Count - index);
                int arrayIndex = RandomTestsUtils.Random.Next(resArray.Length - count + 1);
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
                int index = RandomTestsUtils.Random.Next(array.Count);

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
                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(array.Count - index);

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
                int index = RandomTestsUtils.Random.Next(array.Count);

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
                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(index + 1);

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
                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(array.Count - index);

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
                int index = RandomTestsUtils.Random.Next(array.Count);

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
                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(array.Count - index);

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
                int randomValue = RandomTestsUtils.Random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);
                int index = RandomTestsUtils.Random.Next(array.Count + 1);

                array.Insert(index, randomValue);
                list.Insert(index, randomValue);

                if (!HaveEqualsElements(array, list))
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
                int countElementsInRandomCollection = RandomTestsUtils.Random.Next(10);
                int[] randomCollection = new int[countElementsInRandomCollection];

                for (int j = 0; j < countElementsInRandomCollection; j++)
                    randomCollection[j] = RandomTestsUtils.Random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);

                int index = RandomTestsUtils.Random.Next(array.Count + 1);

                array.InsertRange(index, randomCollection);
                list.InsertRange(index, randomCollection);

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
                int index = RandomTestsUtils.Random.Next(array.Count);

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
                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(index + 1);

                if (array.LastIndexOf(randomElement, index, count) != list.LastIndexOf(randomElement, index, count))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                var randomItem = array.Count == 0 ? RandomTestsUtils.Random.Next() : array[RandomTestsUtils.Random.Next(array.Count)];

                if (array.Remove(randomItem) != list.Remove(randomItem))
                    Assert.Fail();

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();

                randomItem = RandomTestsUtils.Random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);

                if (array.Remove(randomItem) != list.Remove(randomItem))
                    Assert.Fail();

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveAllTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                var randomItem = array.Count == 0 ? RandomTestsUtils.Random.Next() : array[RandomTestsUtils.Random.Next(array.Count)];

                if (array.RemoveAll(item => item <= randomItem) != list.RemoveAll(item => item <= randomItem))
                    Assert.Fail();
                if (!HaveEqualsElements(array, list))
                    Assert.Fail();

                randomItem = RandomTestsUtils.Random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE);

                if (array.RemoveAll(item => item <= randomItem) != list.RemoveAll(item => item <= randomItem))
                    Assert.Fail();
                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                if (array.Count == 0)
                    return;

                int index = RandomTestsUtils.Random.Next(array.Count);

                array.RemoveAt(index);
                list.RemoveAt(index);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void RemoveRangeTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                if (array.Count == 0)
                    return;

                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(Math.Min(10, array.Count - index));

                array.RemoveRange(index, count);
                list.RemoveRange(index, count);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void ReverseTest1()
        {
            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                DynamicArray<int> array = new DynamicArray<int>();
                List<int> list = new List<int>();

                FillCollections(array, list);

                array.Reverse();
                list.Reverse();

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }
        [TestMethod]
        public void ReverseTest2()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                int index = RandomTestsUtils.Random.Next(array.Count);
                int count = RandomTestsUtils.Random.Next(Math.Min(10, array.Count - index));

                array.Reverse(index, count);
                list.Reverse(index, count);

                if (!HaveEqualsElements(array, list))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void ToArrayTest()
        {
            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                DynamicArray<int> array = new DynamicArray<int>();
                List<int> list = new List<int>();

                FillCollections(array, list);

                var arrayRes = array.ToArray();
                var listRes = array.ToArray();

                if (!HaveEqualsElements(arrayRes, listRes))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void TrueForAllTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            if (array.TrueForAll(a => true) != list.TrueForAll(a => true))
                Assert.Fail();

            if (array.TrueForAll(a => false) != list.TrueForAll(a => false))
                Assert.Fail();
        }
    }
}
