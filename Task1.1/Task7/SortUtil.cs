using System;
using System.Collections.Generic;

namespace Task7
{
    internal static class SortUtil
    {
        /// <summary>
        /// Стандартный для большинства языков вид сортировки
        /// Выбирает вид сортировки для рекурсивного вызова в зависимости от длины сортируемого участка и глубины рекурсии
        /// Если длина участка <= 16 - использует сортировку вставками
        /// Иначе если глубина рекурсии больше логарифма от изначальной длины массива, - использует пирамидальную сортировку
        /// Иначе сортирует участок быстрой сортировкой
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            Sorter<T> sorter = new Sorter<T>(list);
            sorter.Sort();
        }

        private class Sorter<T> where T : IComparable<T>
        {
            public double LogOfListSize { get; }

            private readonly IList<T> _list;

            public Sorter(IList<T> list)
            {
                _list = list;
                LogOfListSize = Math.Log(_list.Count, 2);
            }

            public void Sort() => IntroSort(0, _list.Count, 0);

            private void IntroSort(int startIndex, int endIndex, int deep)
            {
                if (endIndex - startIndex <= 16)
                {
                    InsertionSort(startIndex, endIndex);
                    return;
                }
                else if (deep > LogOfListSize)
                {
                    HeapSort(startIndex, endIndex);
                    return;
                }

                int index = Partition(startIndex, endIndex - 1);
                IntroSort(startIndex, index + 1, deep + 1);
                IntroSort(index + 1, endIndex, deep + 1);
            }

            private void InsertionSort(int startIndex, int endIndex)
            {
                for (int i = startIndex + 1; i < endIndex; i++)
                    for (int j = i; j > startIndex && _list[j - 1].CompareTo(_list[j]) == 1; j--)
                        SwapInArrayByIndex(j - 1, j);
            }

            private void HeapSort(int startIndex, int endIndex)
            {
                for (int i = startIndex + (endIndex - startIndex) / 2 - 1; i >= startIndex; i--)
                    Heapify(endIndex - startIndex, i, startIndex, endIndex);

                for (int i = endIndex - 1; i >= startIndex; i--)
                {
                    SwapInArrayByIndex(startIndex, i);
                    Heapify(i - startIndex, startIndex, startIndex, endIndex);
                }
            }

            private void Heapify(int heapSize, int nodeIndex, int startIndex, int endIndex)
            {
                int largestIndex = nodeIndex - startIndex;
                int leftNodeIndex = (nodeIndex - startIndex) * 2 + 1;
                int rightNodeIndex = (nodeIndex - startIndex) * 2 + 2;

                if (leftNodeIndex < heapSize && _list[startIndex + leftNodeIndex].CompareTo(_list[startIndex + largestIndex]) == 1)
                    largestIndex = leftNodeIndex;

                if (rightNodeIndex < heapSize && _list[startIndex + rightNodeIndex].CompareTo(_list[startIndex + largestIndex]) == 1)
                    largestIndex = rightNodeIndex;

                if (largestIndex != nodeIndex - startIndex)
                {
                    SwapInArrayByIndex(nodeIndex, startIndex + largestIndex);
                    Heapify(heapSize, startIndex + largestIndex, startIndex, endIndex);
                }
            }

            private int Partition(int startIndex, int endIndex)
            {
                int middleIndex = startIndex + (endIndex - startIndex) / 2;
                int pivotIndex = GetMedianIndex(startIndex, middleIndex, endIndex);
                SwapInArrayByIndex(pivotIndex, middleIndex);

                T pivot = _list[middleIndex];

                int i = startIndex, j = endIndex;
                while (i <= j)
                {
                    while (_list[i].CompareTo(pivot) == -1)
                        i++;
                    while (_list[j].CompareTo(pivot) == 1)
                        j--;

                    if (i >= j)
                        break;

                    SwapInArrayByIndex(i, j);

                    i++;
                    j--;
                }

                return j;
            }

            private int GetMedianIndex(int index1, int index2, int index3)
            {
                if (_list[index1].CompareTo(_list[index2]) > -1)
                {
                    if (_list[index1].CompareTo(_list[index3]) < 1)
                        return index1;
                    else
                        return _list[index2].CompareTo(_list[index3]) < 1 ? index2 : index3;
                }
                else
                {
                    if (_list[index1].CompareTo(_list[index3]) > -1)
                        return index1;
                    else
                        return _list[index2].CompareTo(_list[index3]) > -1 ? index2 : index3;
                }
            }

            private void SwapInArrayByIndex(int index1, int index2)
            {
                T temp = _list[index1];
                _list[index1] = _list[index2];
                _list[index2] = temp;
            }
        }
    }
}
