using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomCollections
{
    public class DynamicArray<T> : IEnumerable<T>, ICollection<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICloneable
    {
        private const int BASE_CAPACITY = 8;
        private const double TRIM_EXCESS_COUNT_LIMIT = 0.9d;

        private T[] _data;

        public DynamicArray() : this(BASE_CAPACITY) { }
        public DynamicArray(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), $"Argument {nameof(capacity)} can't be negative.");

            Count = 0;

            _data = new T[capacity];
        }
        public DynamicArray(IEnumerable<T> collection) : this()
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"Argument {nameof(collection)} is null.");

            AddRange(collection);
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

            int addCnt = collection.Count();
            if (Count + addCnt > Capacity)
                ResizeArray(Count + addCnt);

            foreach (var value in collection)
            {
                _data[Count] = value;
                Count++;
            }
        }

        public void Clear() => Count = 0;

        public bool Contains(T item)
        {
            foreach (var val in this)
                if (Equals(item, val))
                    return true;

            return false;
        }

        public DynamicArray<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter), $"Argument {nameof(converter)} is null.");

            TOutput[] temp = new TOutput[Count];
            for (int i = 0; i < Count; i++)
                temp[i] = converter(_data[i]);

            return new DynamicArray<TOutput>(temp);
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

            foreach (var val in this)
                if (match(val))
                    return true;

            return false;
        }

        public T Find(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            foreach (var val in this)
                if (match(val))
                    return val;

            return default;
        }

        public DynamicArray<T> FindAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            DynamicArray<T> result = new DynamicArray<T>();
            foreach (var val in this)
                if (match(val))
                    result.Add(val);

            return result;
        }

        public int FindIndex(Predicate<T> match) => FindIndex(0, Count, match);
        public int FindIndex(int startIndex, Predicate<T> match) => FindIndex(startIndex, Count - startIndex, match);
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
                    "-or- startIndex and count do not specify a valid section in the DynamicArray.");

            for (int i = 0; i < count; i++, startIndex++)
                if (match(_data[startIndex]))
                    return startIndex;

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

        public int FindLastIndex(Predicate<T> match) => FindLastIndex(Count - 1, Count, match);
        public int FindLastIndex(int startIndex, Predicate<T> match) => FindLastIndex(startIndex, startIndex + 1, match);
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
                    "-or- index and count do not specify a valid section in the DynamicArray.");

            for (int i = 0; i < count; i++, startIndex--)
                if (match(_data[startIndex]))
                    return startIndex;

            return -1;
        }

        public void ForEach(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action), $"Argument {nameof(action)} is null.");

            try
            {
                foreach (var val in this)
                    action(val);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("An element in the collection has been modified.", e);
            }
        }

        public DynamicArray<T> GetRange(int index, int count) 
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), $"Argument {index} can't be negative.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Argument {count} can't be negative.");

            if (index + count > Count)
                throw new ArgumentException("index and count do not denote a valid range of elements in the DynamicArray.");

            T[] result = new T[count];
            for (int i = 0; i < count; i++, index++)
                result[i] = _data[index];

            return new DynamicArray<T>(result);
        }

        public int IndexOf(T item, int index, int count)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"Argument {index} can't be negative or greater than count of elements - 1.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Argument {count} can't be negative.");

            if (index + count > Count)
                throw new ArgumentException(
                    "index is outside the range of valid indexes for the DynamicArray." +
                    "-or- index and count do not specify a valid section in the DynamicArray.");

            for (int i = 0; i < count; i++, index++)
                if (Equals(item, _data[index]))
                    return index;

            return -1;
        }
        public int IndexOf(T item, int index) => IndexOf(item, index, Count - index);
        public int IndexOf(T item) => IndexOf(item, 0);

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements.");

            Add(item);

            for (int i = Count - 1; i > index; i--)
                _data[i] = _data[i - 1];

            _data[index] = item;
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection), $"Argument {nameof(collection)} is null.");

            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements.");

            int cntNewItems = collection.Count();
            AddRange(collection);

            for (int i = Count - 1, j = i - cntNewItems; j >= index; i--, j--)
                _data[i] = _data[j];

            int currentIndex = index;
            foreach (var item in collection)
            {
                _data[currentIndex] = item;
                currentIndex++;
            }
        }

        public int LastIndexOf(T item) => LastIndexOf(item, Count - 1);
        public int LastIndexOf(T item, int index) => LastIndexOf(item, index, index + 1);
        public int LastIndexOf(T item, int index, int count)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"Argument {index} can't be negative or greater than count of elements - 1.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Argument {count} can't be negative.");

            if (index - count + 1 < 0)
                throw new ArgumentException(
                    "index is outside the range of valid indexes for the DynamicArray." +
                    "-or- index and count do not specify a valid section in the DynamicArray.");

            for (int i = 0; i < count; i++, index--)
                if (Equals(item, _data[index]))
                    return index;

            return -1;
        }

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

        public int RemoveAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            int offset = 0;
            for (int i = 0; i < Count; i++)
            {
                if (match(_data[i]))              
                    offset++;
                else
                    _data[i - offset] = _data[i];
            }

            Count -= offset;

            return offset;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements - 1.");

            for (int i = index + 1; i < Count; i++)
                _data[i - 1] = _data[i];

            Count--;
        }

        public void RemoveRange(int index, int count)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements - 1.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} can't be negative.");

            if (index + count > Count)
                throw new ArgumentException("index and count do not specify a valid section in the DynamicArray.");

            for (int i = index + count; i < Count; i++)
                _data[i - count] = _data[i];

            Count -= count;
        }

        public void Reverse(int index, int count)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"Value of {nameof(index)} can't be negative or greater than count of elements - 1.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} can't be negative.");

            if (index + count > Count)
                throw new ArgumentException("index and count do not specify a valid section in the DynamicArray.");

            for (int i = index, j = index + count - 1; i < j; i++, j--)
            {
                T temp = _data[i];
                _data[i] = _data[j];
                _data[j] = temp;
            }
        }
        public void Reverse() => Reverse(0, Count);

        public T[] ToArray()
        {
            T[] result = new T[Count];

            for (int i = 0; i < Count; i++)
                result[i] = _data[i];

            return result;
        }

        public void TrimExcess()
        {
            if ((Count + .0) / Capacity < TRIM_EXCESS_COUNT_LIMIT)
            {
                T[] temp = new T[Count];

                for (int i = 0; i < Count; i++)
                    temp[i] = _data[i];

                _data = temp;
            }   
        }

        public bool TrueForAll(Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match), $"Argument {nameof(match)} is null.");

            foreach (var item in this)
                if (!match(item))
                    return false;

            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _data[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void ResizeArray(int newCountOfElements)
        {
            int newSize = Capacity == 0 ? 1 : Capacity;

            while (newSize < newCountOfElements)
                newSize <<= 1;

            T[] temp = new T[newSize];

            _data.CopyTo(temp, 0);

            _data = temp;
        }

        public object Clone()
        {
            T[] temp = new T[Capacity];

            for (int i = 0; i < Count; i++)
                temp[i] = this[i];

            return new DynamicArray<T>(temp);
        }
    }
}
