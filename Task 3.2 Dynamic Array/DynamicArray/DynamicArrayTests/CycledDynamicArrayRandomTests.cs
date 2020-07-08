using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static DynamicArrayTests.RandomTestsUtils;

namespace DynamicArrayTests
{
    [TestClass]
    public class CycledDynamicArrayRandomTests
    {
        [TestMethod]
        public void EnumerableTest()
        {
            for (int i = 0; i < DEFAULT_STEPS_COUNT; i++)
            {
                CycledDynamicArray<int> array = new CycledDynamicArray<int>();

                for (int j = 0; j < DEFAULT_COUNT_OF_ELEMENTS; j++)
                    array.Add(_random.Next(DEFAULT_RANDOM_ELEMENT_MIN_VALUE, DEFAULT_RANDOM_ELEMENT_MAX_VALUE));

                int overLimitIterationsCount = 10;
                int cntIterations = 0;
                foreach (var item in array)
                {
                    cntIterations++;
                    if (cntIterations > array.Count + overLimitIterationsCount)
                        break;
                }

                if (cntIterations < array.Count)
                    Assert.Fail();
            }
        }
    }
}
