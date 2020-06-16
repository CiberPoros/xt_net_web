using System;
using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CString
{
    /// <summary>
    /// Represents text as a sequence of UTF-16 code units.
    /// </summary>
    public class CustomString : IEnumerable<char>, IComparable<CustomString>
    {
        /// <summary>
        /// Represents the empty string. This field is read-only.
        /// </summary>
        public static readonly CustomString Empty;

        private static readonly HashSet<CustomString> _internmentsTable;

        private readonly char[] _value;

        static CustomString()
        {
            Empty = new CustomString(new char[] { });

            _internmentsTable = new HashSet<CustomString>
            {
                Empty
            };
        }

        private CustomString(char[] value)
        {
            _value = value; 
        }

        /// <summary>
        /// Gets the number of characters in the current string.
        /// </summary>
        public int Length => _value.Length;

        /// <summary>
        /// Gets the System.Char object at a specified position in the current string.
        /// </summary>
        /// <param name="index">A position in the current string.</param>
        /// <returns>The object at position index.</returns>
        public char this[int index]
        {
            get
            {
                if (index < 0 && index >= _value.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), $"Parameter {nameof(index)} less than zero or greater than or equal to count of elements in string.");

                return _value[index];
            }
        }

        #region INTERNMENT
        /// <summary>
        /// Retrieves the reference to interned string witch is equal to str from internments table.
        /// If internments table doesn't contains specified string, adds the string to internments table.
        /// </summary>
        /// <param name="str">A string to search for in the internments table.</param>
        /// <returns>The reference to interned string witch is equal to str from internments table, if it is interned; 
        /// otherwise, a new reference to a string with the value of str.</returns>
        public static CustomString Intern(CustomString str)
        {
            if (_internmentsTable.TryGetValue(str, out CustomString result))
                return result;

            _internmentsTable.Add(str);
            return str;
        }

        /// <summary>
        /// Retrieves the reference to interned string witch is equal to str from internments table.
        /// </summary>
        /// <param name="str">A string to search for in the internments table.</param>
        /// <returns>The reference to interned string witch is equal to str from internments table, if it is interned; 
        /// otherwise a null value.</returns>
        public static CustomString IsInterned(CustomString str)
        {
            if (_internmentsTable.TryGetValue(str, out CustomString result))
                return result;

            return null;
        } 
        #endregion

        #region CREATE_INSTANCE
        /// <summary>
        /// Initializes a new instance of the CustomString class to the value indicated
        /// by an array of Unicode characters, a starting character position within that
        /// array, and a length.
        /// </summary>
        /// <param name="value">An array of Unicode characters.</param>
        /// <param name="startIndex">The starting position within value.</param>
        /// <param name="length">The number of characters within value to use.</param>
        /// <returns>The new instance of the CustomString class.</returns>
        public static CustomString CreateInstance(char[] value, int startIndex, int length)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), $"Argument {nameof(value)} is null.");

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Value of {nameof(startIndex)} is less than zero.");

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), $"Value of {nameof(length)} is less than zero.");

            if (startIndex + length > value.Length)
                throw new ArgumentOutOfRangeException($"Sum of {nameof(startIndex)} and {nameof(length)} is greater than count of elements in {nameof(value)}");

            var temp = new char[length];
            for (int i = 0; i < length; i++)
                temp[i] = value[i + startIndex];

            CustomString customString = new CustomString(temp);

            return _internmentsTable.TryGetValue(customString, out CustomString internedString) ? internedString : customString;
        }

        /// <summary>
        /// Initializes a new instance of the CustomString class to the Unicode characters
        /// indicated in the specified character array.
        /// </summary>
        /// <param name="value">An array of Unicode characters.</param>
        /// <returns>The new instance of the CustomString class.</returns>
        public static CustomString CreateInstance(char[] value) => CreateInstance(value, 0, value.Length);

        /// <summary>
        /// Initializes a new instance of the CustomString class to the value indicated
        /// by a specified Unicode character repeated a specified number of times.
        /// </summary>
        /// <param name="c">A Unicode character.</param>
        /// <param name="count">The number of times c occurs.</param>
        /// <returns>The new instance of the CustomString class.</returns>
        public static CustomString CreateInstance(char c, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} is less than zero.");

            char[] value = new char[count];
            for (int i = 0; i < count; i++)
                value[i] = c;

            return CreateInstance(value);
        }
        #endregion

        #region TO_CHAR_ARRAY
        /// <summary>
        /// Copies the characters in a specified substring in this instance to a Unicode
        /// character array.
        /// </summary>
        /// <param name="startIndex">The starting position of a substring in this instance.</param>
        /// <param name="length">The length of the substring in this instance.</param>
        /// <returns>A Unicode character array whose elements are the length number of characters
        /// in this instance starting from character position startIndex.</returns>
        public char[] ToCharArray(int startIndex, int length)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Value of {nameof(startIndex)} is less than zero.");

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), $"Value of {nameof(length)} is less than zero.");

            if (startIndex + length > _value.Length)
                throw new ArgumentOutOfRangeException($"Sum of {nameof(startIndex)} and {nameof(length)} is greater than length of this string");

            char[] result = new char[length];
            for (int i = 0; i < length; i++)
                result[i] = _value[i + startIndex];

            return result;
        }

        /// <summary>
        /// Copies the characters in this instance to a Unicode character array.
        /// </summary>
        /// <returns>A Unicode character array whose elements are the individual characters of this
        /// instance. If this instance is an empty string, the returned array is empty and
        /// has a zero length.</returns>
        public char[] ToCharArray() => ToCharArray(0, Length);
        #endregion

        #region FIRST_INDEX_OF
        /// <summary>
        /// Returns the zero-based index position of the first occurrence of the specified
        /// Unicode character in a substring within this instance. The search starts at a
        /// specified character position and proceeds backward toward the beginning of the
        /// string for a specified number of character positions.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">The starting position of the search. The search proceeds from startIndex toward
        /// the beginning of this instance.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it
        /// is not found or if the current instance equals CustomString.Empty.</returns>
        public int FirstIndexOf(char value, int startIndex, int count)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Value of {nameof(startIndex)} is less than zero.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} is less than zero.");

            if (startIndex + count > Length)
                throw new ArgumentOutOfRangeException($"Sum of {nameof(startIndex)} and {nameof(count)} is greater than length of this string");

            for (int i = startIndex; i < startIndex + count; i++)
            {
                if (this[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns the zero-based index position of the first occurrence of a specified Unicode
        /// character within this instance. The search starts at a specified character position
        /// and proceeds backward toward the beginning of the string.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">The starting position of the search. The search proceeds from startIndex toward
        /// the beginning of this instance.</param>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it
        /// is not found or if the current instance equals CustomString.Empty.</returns>
        public int FirstIndexOf(char value, int startIndex) => FirstIndexOf(value, startIndex, Length - startIndex);

        /// <summary>
        /// Returns the zero-based index position of the first occurrence of a specified Unicode
        /// character within this instance.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it
        /// is not.</returns>
        public int FirstIndexOf(char value) => FirstIndexOf(value, 0);

        /// <summary>
        /// Returns the zero-based index position of the first occurrence of a specified string
        /// within this instance. The search starts at a specified character position and
        /// proceeds backward toward the beginning of the string for a specified number of
        /// character positions.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the
        /// beginning of this instance.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The zero-based starting index position of value if that string is found, or -1
        /// if it is not found. If value is CustomString.Empty, the return value is the first index 
        /// position in this instance.</returns>
        public int FirstIndexOf(CustomString value, int startIndex, int count)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), $"Argument {nameof(value)} is null.");

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Value of {nameof(startIndex)} is less than zero.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} is less than zero.");

            if (startIndex + count > Length)
                throw new ArgumentOutOfRangeException($"Sum of {nameof(startIndex)} and {nameof(count)} is greater than length of this string");

            if (this == Empty)
                return -1;

            if (value == Empty)
                return Length - 1;

            for (int i = startIndex; i < startIndex + count && i + value.Length - 1 < Length; i++)
            {
                bool isEqual = true;

                for (int j = 0; j < value.Length; j++)
                {
                    if (this[i + j] != value[j])
                    {
                        isEqual = false;
                        break;
                    }
                }

                if (isEqual)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the zero-based index position of the first occurrence of a specified string
        /// within this instance. The search starts at a specified character position and
        /// proceeds backward toward the beginning of the string.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the
        /// beginning of this instance.</param>
        /// <returns>The zero-based starting index position of value if that string is found, or -1
        /// if it is not found. If value is CustomString.Empty, the return value is the first index 
        /// position in this instance.</returns>
        public int FirstIndexOf(CustomString value, int startIndex) => FirstIndexOf(value, startIndex, Length);

        /// <summary>
        /// Returns the zero-based index position of the first occurrence of a specified string
        /// within this instance.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <returns>The zero-based starting index position of value if that string is found, or -1
        /// if it is not. If value is CustomString.Empty, the return value is the first index 
        /// position in this instance.</returns>
        public int FirstIndexOf(CustomString value) => FirstIndexOf(value, 0);
        #endregion

        #region LAST_INDEX_OF
        /// <summary>
        /// Returns the zero-based index position of the last occurrence of the specified
        /// Unicode character in a substring within this instance. The search starts at a
        /// specified character position and proceeds backward toward the beginning of the
        /// string for a specified number of character positions.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">The starting position of the search. The search proceeds from startIndex toward
        /// the beginning of this instance.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it
        /// is not found or if the current instance equals CustomString.Empty.</returns>
        public int LastIndexOf(char value, int startIndex, int count)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Value of {nameof(startIndex)} is less than zero.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} is less than zero.");

            if (startIndex + count > Length)
                throw new ArgumentOutOfRangeException($"Sum of {nameof(startIndex)} and {nameof(count)} is greater than length of this string");

            for (int i = startIndex + count - 1; i >= startIndex; i--)
            {
                if (this[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns the zero-based index position of the last occurrence of a specified Unicode
        /// character within this instance. The search starts at a specified character position
        /// and proceeds backward toward the beginning of the string.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">The starting position of the search. The search proceeds from startIndex toward
        /// the beginning of this instance.</param>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it
        /// is not found or if the current instance equals CustomString.Empty.</returns>
        public int LastIndexOf(char value, int startIndex) => LastIndexOf(value, startIndex, Length - startIndex);

        /// <summary>
        /// Returns the zero-based index position of the last occurrence of a specified Unicode
        /// character within this instance.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it
        /// is not.</returns>
        public int LastIndexOf(char value) => LastIndexOf(value, 0);

        /// <summary>
        /// Returns the zero-based index position of the last occurrence of a specified string
        /// within this instance. The search starts at a specified character position and
        /// proceeds backward toward the beginning of the string for a specified number of
        /// character positions.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the
        /// beginning of this instance.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The zero-based starting index position of value if that string is found, or -1
        /// if it is not found. If value is CustomString.Empty, the return value is the last index 
        /// position in this instance.</returns>
        public int LastIndexOf(CustomString value, int startIndex, int count)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), $"Argument {nameof(value)} is null.");

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), $"Value of {nameof(startIndex)} is less than zero.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), $"Value of {nameof(count)} is less than zero.");

            if (startIndex + count > Length)
                throw new ArgumentOutOfRangeException($"Sum of {nameof(startIndex)} and {nameof(count)} is greater than length of this string");

            if (this == Empty)
                return -1;

            if (value == Empty)
                return Length - 1;

            for (int i = startIndex + count - value.Length + 1; i >= startIndex; i--)
            {
                bool isEqual = true;

                for (int j = 0; j < value.Length; j++)
                {
                    if (this[i + j] != value[j])
                    {
                        isEqual = false;
                        break;
                    }
                }

                if (isEqual)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the zero-based index position of the last occurrence of a specified string
        /// within this instance. The search starts at a specified character position and
        /// proceeds backward toward the beginning of the string.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position. The search proceeds from startIndex toward the
        /// beginning of this instance.</param>
        /// <returns>The zero-based starting index position of value if that string is found, or -1
        /// if it is not found. If value is CustomString.Empty, the return value is the last index 
        /// position in this instance.</returns>
        public int LastIndexOf(CustomString value, int startIndex) => LastIndexOf(value, startIndex, Length);

        /// <summary>
        /// Returns the zero-based index position of the last occurrence of a specified string
        /// within this instance.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <returns>The zero-based starting index position of value if that string is found, or -1
        /// if it is not. If value is CustomString.Empty, the return value is the last index 
        /// position in this instance.</returns>
        public int LastIndexOf(CustomString value) => LastIndexOf(value, 0);
        #endregion

        #region CUSTOM_METHODS
        /// <summary>
        /// Returns a new string without chars to be removed by predicate.
        /// </summary>
        /// <param name="toRemove">The predicate for definitions chars to remove.</param>
        /// <returns>The new string without chars to be removed by predicate.</returns>
        public CustomString RemoveByPredicate(Predicate<char> toRemove)
        {
            List<char> value = new List<char>();

            foreach (var c in this)
            {
                if (!toRemove(c))
                {
                    value.Add(c);
                }
            }

            return CreateInstance(value.ToArray());
        }

        /// <summary>
        /// Returns a new string with replaced chars by delegate.
        /// </summary>
        /// <param name="func">The predicate for replacement chars.</param>
        /// <returns>The new string with replaced chars by delegate.</returns>
        public CustomString ReplaceByDelegate(Func<char, char> func)
        {
            char[] value = new char[Length];

            for (int i = 0; i < Length; i++)
            {
                value[i] = func(this[i]);
            }

            return CreateInstance(value);
        }
        #endregion

        #region IMPLEMENTATIONS
        public IEnumerator<char> GetEnumerator()
        {
            foreach (var c in _value)
                yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var c in _value)
                yield return c;
        }

        public int CompareTo(CustomString other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), $"Argument {nameof(other)} is null.");

            if (this == other)
                return 0;

            int minLength = Math.Min(Length, other.Length);
            for (int i = 0; i < minLength; i++)
            {
                int compareResult = this[i].CompareTo(other[i]);
                if (compareResult != 0)
                {
                    return compareResult;
                }
            }

            return Length > other.Length ? 1 : -1;
        } 
        #endregion

        #region OVERRIDMENTS
        public override string ToString() => new string(_value);

        public override int GetHashCode()
        {
            if (_value.Length == 0)
                return 0;

            int res = _value[0];

            for (int i = 1; i < _value.Length; i++)
                res ^= _value[i] << (i % 15);

            return res;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var str = obj as CustomString;

            if (str == null)
            {
                return false;
            }

            if (Length != str.Length)
            {
                return false;
            }

            for (int i = 0; i < Length; i++)
            {
                if (this[i] != str[i])
                {
                    return false;
                }
            }

            return true;
        } 
        #endregion

        #region OPERATORS
        public static bool operator ==(CustomString s1, CustomString s2) => s1.Equals(s2);
        public static bool operator !=(CustomString s1, CustomString s2) => !s1.Equals(s2);
        public static bool operator >(CustomString s1, CustomString s2) => s1.CompareTo(s2) > 0;
        public static bool operator <(CustomString s1, CustomString s2) => s1.CompareTo(s2) < 0;
        public static bool operator >=(CustomString s1, CustomString s2) => s1.CompareTo(s2) >= 0;
        public static bool operator <=(CustomString s1, CustomString s2) => s1.CompareTo(s2) <= 0;

        public static CustomString operator +(CustomString s1, CustomString s2)
        {
            char[] value = new char[s1.Length + s2.Length];

            for (int i = 0; i < s1.Length; i++)
                value[i] = s1[i];

            for (int i = 0; i < s2.Length; i++)
                value[s1.Length + i] = s2[i];

            return CreateInstance(value);
        }   
        #endregion
    }
}
