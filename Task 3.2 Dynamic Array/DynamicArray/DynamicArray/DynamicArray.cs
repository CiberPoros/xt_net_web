using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    public class DynamicArray<T> : IEnumerable<T>, ICollection<T>, IList<T>
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

        public T this[int index] 
        { 
            get
            {
                if (index < 0 || index > Count - 1)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements - 1.");

                return _data[index];
            }
            set
            {
                if (index < 0 || index > Count - 1)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements - 1.");

                _data[index] = value;
            }
        }

        private void ResizeArray(int newCountOfElements)
        {
            int newSize = Capacity;

            while (newSize < newCountOfElements)
                newSize <<= 1;

            T[] temp = new T[newSize];

            _data.CopyTo(temp, 0);

            _data = temp;
        }

        #region IENUMERABLE_IMPLEMENTATION
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var val in _data)
                yield return val;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region ICOLLECTION_IMPLEMENTATION
        public void Add(T value)
        {
            if (Count + 1 > Capacity)
                ResizeArray(Count + 1);

            _data[Count] = value;
            Count++;
        }

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

        #region ILIST_IMPLEMENTATION
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (AreEqual(item, _data[i]))
                    return i;

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements.");

            Add(item);

            for (int i = Count - 1; i > index; i--)
                _data[i] = _data[i - 1];

            _data[index] = item;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements - 1.");

            for (int i = index; i < Count - 1; i++)
                _data[i] = _data[i + 1];
        }
        #endregion

        private bool AreEqual(T value1, T value2) =>
            value1 == null && value2 == null || value1 != null && value2 != null && value1.Equals(value2);
    }
}
