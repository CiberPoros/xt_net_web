using System;

namespace ThreeLayer.Common.Utils
{
    public static class Utilites
    {
        public static (Type firstType, Type secondType) SortTypesByName(Type type1, Type type2) =>
            type1.FullName.CompareTo(type2.FullName) == 1 ? (type1, type2) : (type2, type1);
    }
}
