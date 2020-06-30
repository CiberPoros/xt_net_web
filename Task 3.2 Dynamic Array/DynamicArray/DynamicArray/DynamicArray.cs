using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCollections
{
    public class DynamicArray<T> : IEnumerable<T>, ICollection<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
    {
        private T[] _data;

        public DynamicArray() : this(8) { }
        public DynamicArray(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), $"Argument {nameof(capacity)} can't be negative.");

            _data = new T[capacity];
        }
        public DynamicArray(IEnumerable<T> collection) : this()
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"Argument {nameof(collection)} is null.");

            ResizeArray(collection.Count());

            foreach (var value in collection)
                Add(value);
        }

        public int Capacity { get => _data.Length; }
        public int Count { get; private set; }
        public bool IsReadOnly => false;

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

        public void Add(T value)
        {
            if (Count + 1 > Capacity)
                ResizeArray(Count + 1);

            _data[Count] = value;
            Count++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"Argument {nameof(collection)} is null.");

            ResizeArray(Count + collection.Count());

            foreach (var value in collection)
                Add(value);
        }

        public void Clear() => Count = 0;

        public bool Contains(T item)
        {
            foreach (var val in _data)
                if (Equals(item, val))
                    return true;

            return false;
        }

        public DynamicArray<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter), $"Argument {nameof(converter)} is null.");

            return new DynamicArray<TOutput>(_data.Select(value => converter(value)));
        }         

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Argument {nameof(array)} is null.");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"Argument {arrayIndex} can't be negative.");

            if (Count + arrayIndex > array.Length)
                throw new ArgumentException(
                    "Count of elements in the source DynamicArray is greater than the " +
                    "available space from arrayIndex to the end of the destination array.");

            for (int i = 0; i < Count; i++, arrayIndex++)
                array[arrayIndex] = _data[i];
        }
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Argument {nameof(array)} is null.");

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), $"Argument {index} can't be negative.");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"Argument {arrayIndex} can't be negative.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Argument {count} can't be negative.");

            if (index >= Count || Count - index + arrayIndex > array.Length)
                throw new ArgumentException(
                    "index is equal to or greater than the DynamicArray.Count" + 
                    "of the source DynamicArray. -or- The number of elements" +
                    "from index to the end of the source DynamicArray is greater" +
                    "than the available space from arrayIndex to the end of the destination array.");

            for (int i = 0; i < count; i++, arrayIndex++)
                array[arrayIndex] = _data[index + i];
        }
        public void CopyTo(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), $"Argument {nameof(array)} is null.");

            if (Count > array.Length)
                throw new ArithmeticException(
                    "Count of elements in the source DynamicArray is greater than the " +
                    "number of elements that the destination array can contain.");

            CopyTo(array, 0);
        }

        public bool Exists(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            foreach (var val in _data)
                if (match(val))
                    return true;

            return false;
        }

        public T Find(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            foreach (var val in _data)
                if (match(val))
                    return val;

            return default;
        }

        public DynamicArray<T> FindAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            DynamicArray<T> result = new DynamicArray<T>();
            foreach (var val in _data)
                if (match(val))
                    result.Add(val);

            return result;
        }

        public int FindIndex(Predicate<T> match) => FindIndex(0, Count, match);
        public int FindIndex(int startIndex, Predicate<T> match) => FindIndex(startIndex, Count, match);
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Argument {count} can't be negative.");

            if (startIndex < 0 || startIndex > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Argument {startIndex} can't be negative or greater than count of elements - 1.");

            if (startIndex + count > Count)
                throw new ArgumentException(
                    "startIndex is outside the range of valid indexes for the DynamicArray." +
                    "-or- count is less than 0. -or- startIndex and count do not specify a valid section" +
                    "in the DynamicArray.");

            for (int i = startIndex; i < startIndex + count; i++)
                if (match(_data[i]))
                    return i;

            return -1;
        }

        public T FindLast(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            for (int i = Count - 1; i >= 0; i--)
                if (match(_data[i]))
                    return _data[i];

            return default;
        }

        public int FindLastIndex(Predicate<T> match) => FindLastIndex(0, Count, match);
        public int FindLastIndex(int startIndex, Predicate<T> match) => FindLastIndex(startIndex, Count, match);
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Argument {count} can't be negative.");

            if (startIndex < 0 || startIndex > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Argument {startIndex} can't be negative or greater than count of elements - 1.");

            if (startIndex - count + 1 < 0)
                throw new ArgumentException(
                    "index is outside the range of valid indexes for the DynamicArray." +
                    "-or- count is less than 0. -or- index and count do not specify a valid section" +
                    "in the DynamicArray.");

            for (int i = 0; i < count; i++)
                if (match(_data[startIndex - i]))
                    return startIndex - i;

            return -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var val in _data)
                yield return val;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Equals(item, _data[i]))
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


        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (Equals(item, _data[i]))
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


        private void ResizeArray(int newCountOfElements)
        {
            int newSize = Capacity == 0 ? 1 : Capacity;

            while (newSize < newCountOfElements)
                newSize <<= 1;

            T[] temp = new T[newSize];

            _data.CopyTo(temp, 0);

            _data = temp;
        }
    }
}
