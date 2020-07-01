using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomCollections;
using System.Collections.Generic;
using static DynamicArrayTests.Utils;

namespace DynamicArrayTests
{
    [TestClass]
    public class RandomTests
    {
        private const int StepsCount = 1000;
        private static readonly Random _random = new Random();

        [TestMethod]
        public void AddTest()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < StepsCount; i++)
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

            for (int i = 0; i < StepsCount; i++)
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

            for (int i = 0; i < StepsCount; i++)
            {
                int value = _random.Next();
                array.Add(value);
                list.Add(value);

                if (array.Contains(value) != list.Contains(value))
                    Assert.Fail();
            }

            for (int i = 0; i < StepsCount; i++)
            {
                int value = _random.Next();

                if (array.Contains(value) != list.Contains(value))
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

            var resArray = new int[array.Count + 10];
            var resList = new int[list.Count + 10];

            array.CopyTo(resArray, 5);
            list.CopyTo(resList, 5);

            if (!HaveEqualsElements(resArray, resList))
                Assert.Fail();
        }

        [TestMethod]
        public void CopyToTest3()
        {
            DynamicArray<int> array = new DynamicArray<int>();
            List<int> list = new List<int>();

            FillCollections(array, list);

            var resArray = new int[array.Count + 10];
            var resList = new int[list.Count + 10];

            array.CopyTo(3, resArray, 5, StepsCount - 5);
            list.CopyTo(3, resList, 5, StepsCount - 5);

            if (!HaveEqualsElements(resArray, resList))
                Assert.Fail();
        }

        private void FillCollections(DynamicArray<int> dymanicArray, List<int> list)
        {
            for (int i = 0; i < StepsCount; i++)
            {
                int value = _random.Next();
                dymanicArray.Add(value);
                list.Add(value);
            }
        }
    }
}
