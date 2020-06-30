using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    public class DynamicArray<T> : IEnumerable<T>, ICollection<T>
    {
        private T[] _data;

        public DynamicArray(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), $"Argument {nameof(length)} can't be negative.");

            _data = new T[length];
        }

        public DynamicArray() : this(8) { }

        public DynamicArray(IEnumerable<T> values) : this()
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values), $"Argument {nameof(values)} can't be negative.");

            ResizeArray(values.Count());

            foreach (var value in values)
                Add(value);
        }

        public int Capacity { get => _data.Length; }
        public int Count { get; private set; }

        public bool IsReadOnly => throw new NotImplementedException();

        private void ResizeArray(int newCountOfElements)
        {
            int newSize = Capacity;

            while (newSize < newCountOfElements)
                newSize <<= 1;

            T[] temp = new T[newSize];

            _data.CopyTo(temp, 0);

            _data = temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var val in _data)
                yield return val;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T value)
        {
            if (Count + 1 > Capacity)
                ResizeArray(Count + 1);

            _data[Count] = value;
            Count++;
        }

        #region ICOLLECTION_IMPLEMENTATION
        public void Clear() => Count = 0;

        public bool Contains(T item)
        {
            foreach (var val in _data)
                if (AreEqual(item, val))
                    return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Argument {nameof(array)} is null.");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"Argument {arrayIndex} can't be negative.");

            if (Count + arrayIndex > array.Length)
                throw new ArithmeticException(
                    "Count of elements in the source DynamicArray is greater than the " +
                    "available space from arrayIndex to the end of the destination array.");

            for (int i = 0; i < Count; i++, arrayIndex++)
                array[arrayIndex] = _data[i];
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (AreEqual(item, _data[i]))
                {
                    for (int j = i; j < Count - 1; j++)
                    {
                        _data[j] = _data[j + 1];
                    }

                    Count--;
                    return true;
                }
            }

            return false;
        }
        #endregion

        private bool AreEqual(T value1, T value2) =>
            value1 == null && value2 == null || value1 != null && value2 != null && value1.Equals(value2);
    }
}
