using System.Collections.Generic;

namespace CustomCollections
{
    public sealed class CycledDynamicArray<T> : DynamicArray<T>
    {
        public CycledDynamicArray() : base() { }
        public CycledDynamicArray(int capacity) : base(capacity) { }
        public CycledDynamicArray(IEnumerable<T> collection) : base(collection) { }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; ; i = (i + 1) % Count)
                yield return this[i];
        }
    }
}
