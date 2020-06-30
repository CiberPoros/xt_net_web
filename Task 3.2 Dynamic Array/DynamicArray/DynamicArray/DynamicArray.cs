using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomArray
{
    public class DynamicArray<T>
    {
        private T[] _data;

        public DynamicArray(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), $"Argument {nameof(length)} can't be negative.");

            _data = new T[length];
        }

        public int Capacity { get => _data.Length; }
        public int Count { get; private set; }

        public DynamicArray() : this(8) { }

        public DynamicArray(IEnumerable<T> values) : this()
        {
            ResizeArray(values.Count());

            foreach (var value in values)
                Add(value);
        }

        public void Add(T value)
        {
            if (Count + 1 > Capacity)
                ResizeArray(Count + 1);

            _data[Count] = value;
            Count++;
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
    }
}
