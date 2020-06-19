using System;
using System.Text;
using CString;

namespace Task1.TestingStrings
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This is a not interactive program. Just press enter to continue after each step...");
            Console.WriteLine();
            Console.ReadKey();

            WriteCreationExample();
            Console.ReadKey();

            WriteInternmentExample();
            Console.ReadKey();

            WriteOperationsExample();
            Console.ReadKey();

            WriteToCharArrayExamples();
            Console.ReadKey();

            WriteFirstIndexOfExamples();
            Console.ReadKey();

            WriteLastIndexOfExamples();
            Console.ReadKey();

            WriteCustomMethodsExamples();
            Console.ReadKey();
        }

        private static void WriteSeparatorStringsAndAwaitMessage()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.WriteLine();
        }

        private static void WriteCreationExample()
        {
            Console.WriteLine("Examples of creation.");

            Console.WriteLine($"CreateInstance(\"Hello world\") = > {CustomString.CreateInstance("Hello world".ToCharArray())}");
            Console.WriteLine($"CreateInstance(\"Hello world\", 6, 5) => {CustomString.CreateInstance("Hello world".ToCharArray(), 6, 5)}");
            Console.Write("CreateInstance(\"Hello world\", 6, 6) => ");
            try
            {
                Console.Write(CustomString.CreateInstance("Hello world".ToCharArray(), 6, 6));
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Write(nameof(ArgumentOutOfRangeException));
            }
            finally
            {
                Console.WriteLine();
            }
            Console.WriteLine($"CreateInstance('1', 10) => {CustomString.CreateInstance('1', 10)}");

            WriteSeparatorStringsAndAwaitMessage();
        }

        private static void WriteInternmentExample()
        {
            Console.WriteLine("Examples of internment.");

            Console.WriteLine("Without internment:");

            var s1 = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(s1)} = \"{s1}\" created...");

            var s2 = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(s2)} = \"{s2}\" created...");

            Console.WriteLine($"{nameof(s1)}.Equals({nameof(s2)}) => {s1.Equals(s2)}");
            Console.WriteLine($"ReferenceEquals({nameof(s1)},{nameof(s2)}) => {ReferenceEquals(s1, s2)}");
            Console.WriteLine();

            Console.WriteLine("With internment:");
            var s3 = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(s3)} = \"{s3}\" created...");

            s3 = CustomString.Intern(s3);
            Console.WriteLine($"{nameof(s3)} = \"{s3}\" interned...");

            var s4 = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(s4)} = \"{s4}\" created...");

            Console.WriteLine($"{nameof(s3)}.Equals({nameof(s4)}) => {s3.Equals(s4)}");
            Console.WriteLine($"ReferenceEquals({nameof(s3)},{nameof(s4)}) => {ReferenceEquals(s3, s4)}");

            WriteSeparatorStringsAndAwaitMessage();
        }

        private static void WriteOperationsExample()
        {
            Console.WriteLine("Examples of operations.");

            Test(CustomString.CreateInstance("123".ToCharArray()), CustomString.CreateInstance("1234".ToCharArray()));

            Test(CustomString.CreateInstance("aaa".ToCharArray()), CustomString.CreateInstance("aaa".ToCharArray()));

            Test(CustomString.CreateInstance("bb".ToCharArray()), CustomString.CreateInstance("azzzzzz".ToCharArray()));

            WriteSeparatorStringsAndAwaitMessage();

            void Test(CustomString str1, CustomString str2)
            {
                Console.WriteLine($"{str1} + {str2} => {str1 + str2}");
                Console.WriteLine($"{str1} > {str2} => {str1 > str2}");
                Console.WriteLine($"{str1} >= {str2} => {str1 >= str2}");
                Console.WriteLine($"{str1} < {str2} => {str1 < str2}");
                Console.WriteLine($"{str1} <= {str2} => {str1 <= str2}");
                Console.WriteLine($"{str1} == {str2} => {str1 == str2}");
                Console.WriteLine();
            } 
        }

        private static void WriteToCharArrayExamples()
        {
            Console.WriteLine("Examples of overloads of method \"ToCharArray\".");

            CustomString str = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(str)} = CreateInstance(\"{str}\")");

            Console.WriteLine($"str.ToCharArray() => {ArrayToString(str.ToCharArray())}");
            Console.WriteLine($"str.ToCharArray(6, 5) => {ArrayToString(str.ToCharArray(6, 5))}");
            Console.Write("str.ToCharArray(6, 6) => ");
            try
            {
                Console.Write(ArrayToString(str.ToCharArray(6, 6)));
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Write(nameof(ArgumentOutOfRangeException));
            }
            finally
            {
                Console.WriteLine();
            }

            string ArrayToString(char[] arr)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var val in arr)
                {
                    stringBuilder.Append($"[{val}] ");
                }

                return stringBuilder.ToString();
            }

            WriteSeparatorStringsAndAwaitMessage();
        }

        private static void WriteFirstIndexOfExamples()
        {
            Console.WriteLine("Examples of overloads of method \"WriteFirstIndexOf\".");

            CustomString str = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(str)} = CreateInstance(\"{str}\")");

            Console.WriteLine($"str.FirstIndexOf('w') => {str.FirstIndexOf('w')}");
            Console.WriteLine($"str.FirstIndexOf('k') => {str.FirstIndexOf('k')}");

            Console.WriteLine($"str.FirstIndexOf('w', 6) => {str.FirstIndexOf('w', 6)}");
            Console.WriteLine($"str.FirstIndexOf('w', 8) => {str.FirstIndexOf('w', 8)}");

            Console.WriteLine($"str.FirstIndexOf('w', 3, 4) => {str.FirstIndexOf('w', 3, 4)}");
            Console.WriteLine($"str.FirstIndexOf('w', 3, 2) => {str.FirstIndexOf('w', 3, 2)}");

            CustomString requiredStr1 = CustomString.CreateInstance("Hello".ToCharArray());
            CustomString requiredStr2 = CustomString.CreateInstance("hello".ToCharArray());
            CustomString requiredStr3 = CustomString.CreateInstance("rl".ToCharArray());
            CustomString requiredStr4 = CustomString.CreateInstance("rld".ToCharArray());

            Console.WriteLine($"str.FirstIndexOf(\"{requiredStr1}\") => {str.FirstIndexOf(requiredStr1)}");
            Console.WriteLine($"str.FirstIndexOf(\"{requiredStr2}\") => {str.FirstIndexOf(requiredStr2)}");
            Console.WriteLine($"str.FirstIndexOf(\"{requiredStr3}\") => {str.FirstIndexOf(requiredStr3)}");
            Console.WriteLine($"str.FirstIndexOf(\"{requiredStr4}\") => {str.FirstIndexOf(requiredStr4)}");

            Console.WriteLine($"str.FirstIndexOf(\"{requiredStr3}\", 0, 9) => {str.FirstIndexOf(requiredStr3, 0, 9)}");
            Console.WriteLine($"str.FirstIndexOf(\"{requiredStr3}\", 0, 8) => {str.FirstIndexOf(requiredStr3, 0, 8)}");

            WriteSeparatorStringsAndAwaitMessage();
        }

        private static void WriteLastIndexOfExamples()
        {
            Console.WriteLine("Examples of overloads of method \"WriteLastIndexOf\".");

            CustomString str = CustomString.CreateInstance("Hello world".ToCharArray());
            Console.WriteLine($"{nameof(str)} = CreateInstance(\"{str}\")");

            Console.WriteLine($"str.LastIndexOf('l') => {str.LastIndexOf('l')}");
            Console.WriteLine($"str.LastIndexOf('k') => {str.LastIndexOf('k')}");

            Console.WriteLine($"str.LastIndexOf('w', 6) => {str.LastIndexOf('w', 6)}");
            Console.WriteLine($"str.LastIndexOf('w', 8) => {str.LastIndexOf('w', 8)}");

            Console.WriteLine($"str.LastIndexOf('w', 3, 4) => {str.LastIndexOf('w', 3, 4)}");
            Console.WriteLine($"str.LastIndexOf('w', 3, 2) => {str.LastIndexOf('w', 3, 2)}");

            CustomString requiredStr1 = CustomString.CreateInstance("Hello".ToCharArray());
            CustomString requiredStr2 = CustomString.CreateInstance("hello".ToCharArray());
            CustomString requiredStr3 = CustomString.CreateInstance("rl".ToCharArray());
            CustomString requiredStr4 = CustomString.CreateInstance("rld".ToCharArray());

            Console.WriteLine($"str.LastIndexOf(\"{requiredStr1}\") => {str.LastIndexOf(requiredStr1)}");
            Console.WriteLine($"str.LastIndexOf(\"{requiredStr2}\") => {str.LastIndexOf(requiredStr2)}");
            Console.WriteLine($"str.LastIndexOf(\"{requiredStr3}\") => {str.LastIndexOf(requiredStr3)}");
            Console.WriteLine($"str.LastIndexOf(\"{requiredStr4}\") => {str.LastIndexOf(requiredStr4)}");

            Console.WriteLine($"str.LastIndexOf(\"{requiredStr3}\", 0, 9) => {str.LastIndexOf(requiredStr3, 0, 9)}");
            Console.WriteLine($"str.LastIndexOf(\"{requiredStr3}\", 0, 8) => {str.LastIndexOf(requiredStr3, 0, 8)}");

            WriteSeparatorStringsAndAwaitMessage();
        }

        private static void WriteCustomMethodsExamples()
        {
            Console.WriteLine("Examples of custom methods.");

            CustomString str = CustomString.CreateInstance("HeLLo wOrld".ToCharArray());
            Console.WriteLine($"{nameof(str)} = CreateInstance(\"{str}\")");

            Console.WriteLine($"str.RemoveByPredicate(c => char.IsUpper(c)) => {str.RemoveByPredicate(c => char.IsUpper(c))}");
            Console.WriteLine($"str.RemoveByPredicate(c => c == 'L') => {str.RemoveByPredicate(c => c == 'L')}");
            Console.WriteLine($"str.ReplaceByDelegate(c => char.ToLower(c)) => {str.ReplaceByDelegate(c => char.ToLower(c))}");
            Console.WriteLine($"str.ReplaceByDelegate(c => char.ToLower(c) == 'l' ? '1' : c)) => {str.ReplaceByDelegate(c => char.ToLower(c) == 'l' ? '1' : c)}");

            WriteSeparatorStringsAndAwaitMessage();
        }
    }
}
